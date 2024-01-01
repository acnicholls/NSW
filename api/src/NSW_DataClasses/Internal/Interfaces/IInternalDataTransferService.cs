using IdentityModel.Client;

namespace NSW.Data.Internal.Interfaces
{
	public interface IInternalDataTransferService
	{
		Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync();
		Task<string> GetUserTokenAsync();
		Task<string> GetClientTokenAsync(string clientId = "default");
		Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType);
	}
}
