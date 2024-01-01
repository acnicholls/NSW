using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;


namespace NSW.Idp
{
	using IdentityServer4.Configuration;
	using NSW.Data.Validation;
	using NSW.Data.Validation.Interfaces;
	using NSW.Idp.Configuration;
	using NSW.Idp.Data;
	using NSW.Idp.Models;

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
			var oidcOptions = NSW.Data.Extensions.DependencyInjection.RegisterServices(services, Configuration, DataTransferVaraintEnum.Tools);
			NSW.Data.Extensions.DependencyInjection.RegisterPostalTask(services);
			IdentityModelEventSource.ShowPII = true;
			services.AddControllersWithViews();

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString(Configuration.GetSection("ConnectionString").Value)));

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
						"https://idp:5007",
						"https://localhost:3000",
						"http://localhost:3000"
						)
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});

			services.AddTransient<IdentityServer4.Services.ICorsPolicyService, CorsPolicyService>();

			var options = new IdentityServerOptions
			{
				IssuerUri = oidcOptions.Authority,
				Events = new EventsOptions
				{
					RaiseErrorEvents = true,
					RaiseInformationEvents = true,
					RaiseFailureEvents = true,
					RaiseSuccessEvents = true,
				},
				EmitStaticAudienceClaim = true,
			};
			services.AddSingleton<IdentityServerOptions>(options);

			var builder = services.AddIdentityServer(options => { })
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

			app.UseCors();

			app.UseIdentityServer();
			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}