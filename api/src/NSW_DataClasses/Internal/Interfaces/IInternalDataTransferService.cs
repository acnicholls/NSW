using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace NSW.Data.Internal.Interfaces
{
	public interface IInternalDataTransferService
	{
		Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync();
		Task<string> GetUserTokenAsync();
		Task<string> GetClientTokenAsync(string clientId = "default");
		Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType);

		Task<string> GetUserTokenAsync(HttpContext context);
		Task<string> GetClientTokenAsync(HttpContext context, string clientId = "default");
		Task<T> GetDataFromApiAsync<T>(HttpContext context, string apiEndpointPartialUrl, ApiAccessType accessType);
	}
}
