using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ProxyKit;
using Starter.Bff.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Starter.Bff
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;


        public Startup()
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // TODO: Load config options here
            // authority
            // internal url
            // metadata url
            // clientId
            // clientSecret

            IdentityModelEventSource.ShowPII = true;
            services.AddProxy();
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
                options.Authority = "https://localhost";
                options.ClientId = "NSW.Bff";
                options.ClientSecret = "secret";
                options.MetadataAddress = "http://idp:5006/.well-known/openid-configuration";
                options.RequireHttpsMetadata = false;
                // options.CallbackPath = "http://bff:5004/signin-oidc";

                options.ResponseType = "code";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;

                // options.Events = new OpenIdConnectEvents
                // {
                //     OnRedirectToIdentityProvider = async context =>
                //     {
                //         context.ProtocolMessage.State = context.HttpContext.Request.Path.Value.ToString();
                //     },
                //     OnTokenValidated = ctx =>
                //     {
                //         var url = ctx.ProtocolMessage.GetParameter("state");
                //         var claims = new List<Claim>
                //         {
                //             new Claim("returnUrl ", url)
                //         };
                //         var appIdentity = new ClaimsIdentity(claims);

                //         //add url to claims
                //         ctx.Principal.AddIdentity(appIdentity);

                //         return Task.CompletedTask;
                //     },
                //     OnTicketReceived = ctx =>
                //     {
                //         var url = ctx.Principal.FindFirst("returnUrl").Value;
                //         ctx.ReturnUri = url;
                //         return Task.CompletedTask;
                //     }

                // };

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("NSW.ApiScope");  // this is the API from this solution
                options.Scope.Add("offline_access");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };

            });

            //services.AddSingleton<IDiscoveryCache>(r =>
            //{
            //    var factory = r.GetRequiredService<IHttpClientFactory>();
            //    return new DiscoveryCache(
            //        "http://idp:5006", 
            //        () => factory.CreateClient(), 
            //        new DiscoveryPolicy
            //        { 
            //            RequireHttps = false,
            //        });
            //});

            var cache = new DiscoveryCache(
                "http://idp:5006",
                new DiscoveryPolicy
                {
                    RequireHttps = false,
                    ValidateIssuerName = false
                });
            services.AddSingleton<IDiscoveryCache>(cache);

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

            app.UseMiddleware<StrictSameSiteExternalAuthenticationMiddleware>();
            app.UseAuthentication();

            app.UseForwardedHeaders();

            // // rewrite outgoing redirects to the Identity Provider, as though they were for an external address
            app.Use(async (httpcontext, next) =>
            {
                await next();
                if (httpcontext.Response.StatusCode == StatusCodes.Status302Found)
                {
                    string location = httpcontext.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location];
                    httpcontext.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location] =
                            location.Replace("://idp:5006/", "://localhost/");
                }

            });

            // challenge any unauthenticated user
            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    await context.ChallengeAsync(new AuthenticationProperties { RedirectUri = "https://localhost/loggedin" });
                    return;
                }

                await next();
            });

            // app.UseForwardedHeaders(new ForwardedHeadersOptions
            // {
            //     ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
            // });
            // if the request is for the "api" subfolder, attach an access token and proxy the request to the api
            app.Map("/api", api =>
            {
                api.RunProxy(async context =>
                {
                    var forwardContext = context.ForwardTo("https://api:5003");

                    var token = await context.GetUserAccessTokenAsync();
                    forwardContext.UpstreamRequest.SetBearerToken(token);

                    return await forwardContext.Send();
                });
            });

            // any UI components should come from the files on the server
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // use the url to determine the files to use to process the request
            app.UseRouting();

            // // process authentication and authorization of the response to the earlier challenge
            app.UseAuthentication();
            app.UseAuthorization();

            // create route endpoints from all the ApiController/Controller classes registered
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers()
                        .RequireAuthorization();
                });
        }
    }
}