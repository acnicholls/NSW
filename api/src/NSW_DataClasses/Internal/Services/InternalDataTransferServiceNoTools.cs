using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;


namespace NSW.Data.Internal.Services
{
	public class InternalDataTransferServiceNoTools : InternalDataTransferServiceBase, IInternalDataTransferService
	{

		public InternalDataTransferServiceNoTools(
			IHttpContextAccessor httpContextAccessor,
			IDiscoveryCache discoveryCache,
			ILogger<InternalDataTransferService> logger,
			IConfiguration configuration,
			OidcOptions oidcOptions
		) : base(httpContextAccessor, discoveryCache, logger, configuration, oidcOptions)
		{
		}
	}
}
