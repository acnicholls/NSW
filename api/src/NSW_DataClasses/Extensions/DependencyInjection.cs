using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSW.Data.Interfaces;
using NSW.Data.Internal.Services;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;
using IdentityServer4;
using System.Runtime.CompilerServices;
using NSW.Data.Validation.Interfaces;
using NSW.Data.Validation;

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

		public static void RegisterPostalTask(IServiceCollection services)
		{
			// custom task that uses the IDTS
			services.AddTransient<IPostalCodeTask, PostalCodeTask>();
		}

		public static OidcOptions RegisterServices(IServiceCollection services, IConfiguration configuration, DataTransferVaraintEnum variant)
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
					ValidateIssuerName = false  // TODO: this is development settings
				});
			services.AddSingleton<IDiscoveryCache>(cache);

			// InternalDataTransferService -- for getting info from API to BFF/IDP
			switch(variant)
			{
				case DataTransferVaraintEnum.Tools:
					{
						RegisterDataTransferServiceWithTools(services);
						break;
					}
				case DataTransferVaraintEnum.NoTools:
					{
						RegisterDataTransferServiceWithNoTools(services);
						break;
					}
				default:
					{
						RegisterDataTransferServiceWithNoTools(services);
						break;
					}
			}


			RegisterTestUser(services);



			return oidcOptions;
		}

		private static void RegisterDataTransferServiceWithTools(IServiceCollection services)
		{
			services.AddTransient<IdentityServer4.IdentityServerTools>();
			services.AddTransient<IInternalDataTransferService, InternalDataTransferService>();
		}

		private static void RegisterDataTransferServiceWithNoTools(IServiceCollection services)
		{
			services.AddTransient<IInternalDataTransferService, InternalDataTransferServiceNoTools>();
		}
	}
}
