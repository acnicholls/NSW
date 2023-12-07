using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.Extensions.Logging;


namespace Starter.Idp
{
    using Starter.Idp.Configuration;
    using Starter.Idp.Custom;
    using Starter.Idp.Data;
    using Starter.Idp.Models;

    public class Startup
    {
        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public Startup(
            IWebHostEnvironment environment,
            IConfiguration config
            )
        {
            Environment = environment;
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(
                        "http://localhost",
                        "https://localhost",
                        "http://bff:5004",
                        "https://bff:5005",
                        "http://api:5002",
                        "https://api:5003",
                        "http://idp:5006",
                        "https://idp:5007"
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddTransient<IdentityServer4.Services.ICorsPolicyService, CorsPolicyService>();

            var builder = services.AddIdentityServer(options =>
             {
                 // need to set IdentityServer's external address, but allow the server to be connected to internally as well.
                 // options.SiteName = "Starter";
                 // options.PublicOrigin = "http://localhost:5006";
                 options.IssuerUri = "https://localhost";

                 // options.RequireSsl = false;
                 // options.RequireHttpsMetadata = false;
                 options.Events.RaiseErrorEvents = true;
                 options.Events.RaiseInformationEvents = true;
                 options.Events.RaiseFailureEvents = true;
                 options.Events.RaiseSuccessEvents = true;

                 // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                 options.EmitStaticAudienceClaim = true;
             })
                 .AddInMemoryIdentityResources(Config.IdentityResources)
                 .AddInMemoryApiScopes(Config.ApiScopes)
                 .AddInMemoryClients(Config.Clients)
                 .AddInMemoryApiResources(Config.ApiResources)
                 .AddAspNetIdentity<ApplicationUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
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
            if (Environment.EnvironmentName == "Development")
            {
                SeedData.EnsureSeedData(Configuration.GetConnectionString(Configuration.GetSection("ConnectionString").Value));
            }
            app.UseForwardedHeaders();
            //Add our new middleware to the pipeline
            // app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseIdentityServer();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}