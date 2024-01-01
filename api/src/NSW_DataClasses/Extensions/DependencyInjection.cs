using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSW.Data.Interfaces;
using NSW.Data.Internal;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;
using IdentityServer4;
using System.Runtime.CompilerServices;

namespace NSW.Data.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterTestUser(IServiceCollection services)
		{
			// TODO: only add this if running from a test assembly?
			var user = new User("test", "Test@acnicholls.com");
			services.AddScoped<IUser>((services) =>
			{
				return user;
			});

		}

		public static OidcOptions RegisterOidcOptions(IServiceCollection services, IConfiguration configuration)
		{
			var oidcOptions = new OidcOptions();
			configuration.Bind("Authentication", oidcOptions);
			services.AddSingleton<OidcOptions>(oidcOptions);
			return oidcOptions;
		}

		public static OidcOptions RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			// validate parameters
			if(configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var oidcOptions = RegisterOidcOptions(services, configuration);	


			// TODO: register the services 
			var cache = new DiscoveryCache(
				oidcOptions.Authority,
				new DiscoveryPolicy
				{
					RequireHttps = false,
					ValidateIssuerName = false
				});
			services.AddSingleton<IDiscoveryCache>(cache);
			services.AddTransient<IdentityServer4.IdentityServerTools>();
			// InternalDataTransferService -- for getting info from API to BFF/IDP
			services.AddTransient<IInternalDataTransferService, InternalDataTransferService>();

			RegisterTestUser(services);

			return oidcOptions;
		}
	}
}
