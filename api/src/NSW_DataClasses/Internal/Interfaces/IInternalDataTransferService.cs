using IdentityModel.Client;

namespace NSW.Data.Internal.Interfaces
{
    public interface IInternalDataTransferService
	{
		Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync();
		Task<string> GetUserTokenAsync();

		Task<string> GetTokenStringAsync(ApiAccessType accessType);

		Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType);
		Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, string token);
        Task<TOutput> PostDataToApiAsync<TInput, TOutput>(string apiEndpointPartialUrl, string token, TInput data);
        Task<TOutput> PostDataToApiAsync<TInput, TOutput>(string apiEndpointPartialUrl, ApiAccessType accessType, TInput data);
    }
}
