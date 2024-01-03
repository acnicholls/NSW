using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;


namespace NSW.Data.Internal.Services
{
    /// <summary>
    /// this is a pass-thru implementation, just so the one going into the DI isn't called "Base"
    /// It is intended for use by the BFF to request specific routes from either the API or the IDP
    /// </summary>
	public class InternalDataTransferServiceNoTools : InternalDataTransferServiceBase, IInternalDataTransferService
	{

		public InternalDataTransferServiceNoTools(
			IHttpContextAccessor httpContextAccessor,
			IDiscoveryCache discoveryCache,
			ILogger<InternalDataTransferService> logger,
			IConfiguration configuration,
			OidcOptions oidcOptions,
            IHttpClientFactory httpClientFactory
        ) : base(httpContextAccessor, discoveryCache, logger, configuration, oidcOptions, httpClientFactory)
		{
		}
	}
}
