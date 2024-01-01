using IdentityModel.Client;
using IdentityServer4;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;
using System.Security.Claims;


namespace NSW.Data.Internal.Services
{
	public class InternalDataTransferService : InternalDataTransferServiceBase, IInternalDataTransferService
	{
		private readonly IdentityServerTools _tools;

		public InternalDataTransferService(
			IdentityServerTools tools,
			IHttpContextAccessor httpContextAccessor,
			IDiscoveryCache discoveryCache,
			ILogger<InternalDataTransferService> logger,
			IConfiguration configuration,
			OidcOptions oidcOptions
		) : base(httpContextAccessor, discoveryCache, logger, configuration, oidcOptions)
		{
			_tools = tools;
		}


		protected override async Task<string> GetTokenStringAsync(ApiAccessType accessType)
		{
			var tokenString = string.Empty;
			try
			{
				switch(accessType)
				{
					case ApiAccessType.Client:
						{
							tokenString = await this.GetUserTokenAsync();
							break;
						}
					case ApiAccessType.User:
						{
							tokenString = await this.GetClientTokenAsync(_oidcOptions.ClientId);
							break;
						}
					case ApiAccessType.Idp:
						{
							tokenString = await _tools.IssueJwtAsync(600, _oidcOptions.Authority, new List<Claim>
							{
								new Claim("sub", "-1"),
								new Claim("role", "MEMBER"),
							});
							break;
						}
					default:
						{
							break;
						}
				}
			}
			catch (Exception)
			{

				throw;
			}
			return tokenString;
		}
	}
}
