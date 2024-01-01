using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace NSW.Data.Internal.Interfaces
{
	public interface IInternalDataTransferService
	{
		Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync();
		Task<string> GetUserTokenAsync();

		Task<string> GetTokenStringAsync(ApiAccessType accessType);

		Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType);
		Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, string token);
	}
}
