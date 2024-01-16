using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NSW.Bff.Internal;
using NSW.Data.Validation.Interfaces;
using ProxyKit;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Serilog;
using ILogger = Serilog.ILogger;

namespace NSW.Bff
{
    public class Startup
    {
        private readonly ILogger  _logger;
        private readonly IConfiguration _configuration;


        //public Startup()
        //{
        //    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        //}

        public Startup(
            IConfiguration configuration)
        {
            _configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            _logger = Log.Logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // capture the settings.
            var oidcOptions = NSW.Data.Extensions.DependencyInjection.RegisterServices(services, _configuration, DataTransferVaraintEnum.NoTools);
            NSW.Data.Extensions.DependencyInjection.RegisterPostalTask(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(
                        "http://localhost",
                        "https://localhost",
                        "http://api:5002",
                        "https://api:5003",
                        "http://idp:5006",
                        "https://idp:5007",
                        "http://localhost:3000",
                        "https://localhost:3000"
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            IdentityModelEventSource.ShowPII = true;
            services.AddProxy();
            services.AddLogging();
            services.AddAccessTokenManagement();

            services.AddControllers();
            services.AddDistributedMemoryCache();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookies", options =>
            {
                options.Cookie.Name = "NSW.bff";
                options.Cookie.SameSite = SameSiteMode.Strict;
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = oidcOptions.Authority;
                options.ClientId = oidcOptions.ClientId;
                options.ClientSecret = oidcOptions.ClientSecret;
                options.MetadataAddress = oidcOptions.MetadataAddress;
                options.RequireHttpsMetadata = oidcOptions.RequireHttpsMetadata;
                // options.CallbackPath = oidcOptions.CallbackPath;

                options.ResponseType = oidcOptions.ResponseType;
                options.GetClaimsFromUserInfoEndpoint = oidcOptions.GetClaimsFromUserInfoEndpoint;
                options.SaveTokens = oidcOptions.SaveTokens;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                //options.Scope.Add("roles");
                //options.Scope.Add("email");
                //options.Scope.Add("phone");
                options.Scope.Add("NSW.ApiScope");  // this is the API from this solution
                options.Scope.Add("offline_access");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };

            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.Use(async (context, next) =>
            {
                if (context is not null)
                {
                    if (!NSW.Data.Validation.ValidPostalCodes.NaganoPostalCodes.Any())
                    {
                        var postalCodeTask = app.ApplicationServices.GetRequiredService<IPostalCodeTask>();
                        postalCodeTask.StartBackgroundPostalCodeWorker(ApiAccessType.Client);
                    }
                }
                await next(context);
            });

            // the IDP has sent an "access_denied" authentication message, capture and handle it.
            app.UseMiddleware<NswIdpAccessDeniedMiddleware>();

            app.UseMiddleware<StrictSameSiteExternalAuthenticationMiddleware>();
            app.UseAuthentication();

            app.UseForwardedHeaders();

            // // rewrite outgoing redirects to the Identity Provider, as though they were for an external address
            app.Use(async (httpcontext, next) =>
            {
                await next(httpcontext);
                if (httpcontext.Response.StatusCode == StatusCodes.Status302Found)
                {
                    var oldPart = _configuration.GetValue<string>("Authentication:InternalAddressPart");
                    var newPart = _configuration.GetValue<string>("Authentication:ExternalAddressPart");

                    string location = httpcontext.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location];
                    httpcontext.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location] =
                            location.Replace(oldPart, newPart);
                }

            });


            // challenge any unauthenticated user
            app.Use(async (context, next) =>
            {
                // allow some local BFF routes to be accessed by anonymous users
                var query = context.Request.Path.ToString();
                _logger.Debug($"Checking if authentication is required on {query}");
                var skipChallenge = false;
                var anonEndpoints = new List<string> { "/bff/Post", "/bff/LabelText", "/bff/PostCategory" };
                foreach (var endpoint in anonEndpoints)
                {
                    if (query.StartsWith(endpoint))
                    {
                        skipChallenge = true;
                    }
                }
                // if the user is NOT authenticated and trying to access an endpoint that requires authentication, challenge them.
                if (!context.User.Identity.IsAuthenticated && !skipChallenge)
                {
                    await context.ChallengeAsync(new AuthenticationProperties { RedirectUri = _configuration.GetValue<string>("Authentication:LoggedInRedirect") });
                    return;
                }

                await next(context);
            });



            // any UI components should come from the files on the server
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // use the url to determine the files to use to process the request
            app.UseRouting();

            app.UseCors("CorsPolicy");

            // // process authentication and authorization of the response to the earlier challenge
            app.UseAuthentication();
            app.UseAuthorization();

            app.Map("/api", api =>
            {
                api.RunProxy(async context =>
                {
                    var apiUrl = $"{_configuration.GetValue<string>("Api:BaseUrl")}/api";

                    var forwardContext = context.ForwardTo(apiUrl);

                    var token = await context.GetUserAccessTokenAsync();
                    forwardContext.UpstreamRequest.SetBearerToken(token);

                    return await forwardContext.Send();
                });
            });

            // create route endpoints from all the ApiController/Controller classes registered
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers()
                        .RequireAuthorization();
                });
        }
    }
}