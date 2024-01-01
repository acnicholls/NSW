using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;
using System.Text.Json;


namespace NSW.Data.Internal.Services
{
	public class InternalDataTransferServiceBase : IInternalDataTransferService
	{
		protected readonly ILogger<InternalDataTransferService> _logger;

		protected readonly IDiscoveryCache _discoveryCache;

		protected readonly IHttpContextAccessor _httpContextAccessor;
		protected readonly IConfiguration _configuration;
		protected readonly OidcOptions _oidcOptions;

		public InternalDataTransferServiceBase(
			IHttpContextAccessor httpContextAccessor,
			IDiscoveryCache discoveryCache,
			ILogger<InternalDataTransferService> logger,
			IConfiguration configuration,
			OidcOptions oidcOptions
		)
		{
			_logger = logger;
			_discoveryCache = discoveryCache;
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
			_oidcOptions = oidcOptions;
		}
		protected DiscoveryDocumentResponse _discoveryDocument;

		protected HttpContext GetContextFromAccessor()
		{
			_logger.LogTrace("Starting GetContextFromAccessor()");
			var context = _httpContextAccessor.HttpContext;
			if (context is null)
			{
				_logger.LogTrace("context is null, throwing error...");
				throw new ArgumentNullException(nameof(_httpContextAccessor.HttpContext));
			}
			_logger.LogTrace("Completing GetContextFromAccessor()");
			return context;
		}

		public async Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync()
		{
			_logger.LogTrace("Starting GetIdpDiscoveryDocumentAsync()");
			if (_discoveryDocument != null)
			{
				_logger.LogTrace("Disocvery Document already cached, returning cached copy...");
				return _discoveryDocument;
			}
			_logger.LogTrace("Disocvery Document null, acquiring from IDP");
			_discoveryDocument = await _discoveryCache.GetAsync();

			_logger.LogDebug("DisocveryDocumentResponse.IsError {0}", _discoveryDocument.IsError);
			_logger.LogDebug("DisocveryDocumentResponse.Error {0}", _discoveryDocument.Error);
			_logger.LogDebug("DisocveryDocumentResponse.ErrorType {0}", _discoveryDocument.ErrorType);
			_logger.LogDebug("DisocveryDocumentResponse.Exception {0}", _discoveryDocument.Exception);

			_logger.LogDebug("DisocveryDocumentResponse.HttpErrorReason {0}", _discoveryDocument.HttpErrorReason);
			_logger.LogDebug("DisocveryDocumentResponse.HttpStatusCode {0}", _discoveryDocument.HttpStatusCode);

			if (_discoveryDocument == null)
			{
				_logger.LogInformation("_discoveryDocument is null");
			}

			if (_discoveryDocument.IsError)
			{
				_logger.LogError(_discoveryDocument.Exception, "InternalDataTransferService.GetIdpDiscoveryCacheAsync");
				throw new System.Exception("error getting discovery cache.");
			}


			_logger.LogDebug("DisocveryDocumentResponse {0}", _discoveryDocument.TokenEndpoint);
			_logger.LogDebug("DisocveryDocumentResponse {0}", _discoveryDocument.UserInfoEndpoint);
			_logger.LogTrace("Completing GetIdpDiscoveryDocumentAsync()");
			return _discoveryDocument;
		}

		public async Task<string> GetUserTokenAsync()
		{
			_logger.LogTrace("Starting GetUserTokenAsync()");
			// grab the DI'd context
			var context = GetContextFromAccessor();
			_logger.LogTrace("Got HttpContext from Accessor");
			// pass it to the local method.
			_logger.LogTrace("Completing GetUserTokenAsync()");
			return await this.GetUserTokenAsync(context);
		}
		protected async Task<string> GetUserTokenAsync(HttpContext context)
		{
			_logger.LogTrace("Starting GetUserTokenAsync(HttpContext)");
			var returnValue = string.Empty;
			try
			{
				returnValue = await context.GetUserAccessTokenAsync();
				var messageValue = true ? returnValue : "***REDACTED***";  // TODO: find environment value, set to false for prod
				_logger.LogTrace("Got token value {messageValue}, returning...", messageValue);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetUserTokenAsync(HttpContext)");
			}
			_logger.LogTrace("Completing GetUserTokenAsync(HttpContext)");
			return returnValue;
		}
		protected async Task<string> GetClientTokenAsync(string clientId = "default")
		{
			_logger.LogTrace("Starting GetClientTokenAsync()");
			// grab the DI'd context
			var context = GetContextFromAccessor();
			_logger.LogTrace("Got HttpContext from Accessor");
			// pass it to the local method.
			_logger.LogTrace("Completing GetClientTokenAsync()");
			return await this.GetClientTokenAsync(context);
		}

		protected async Task<string> GetClientTokenAsync(HttpContext context, string clientId = "default")
		{
			_logger.LogTrace("Starting GetClientTokenAsync(HttpContext)");
			var returnValue = string.Empty;
			try
			{
				returnValue = await context.GetClientAccessTokenAsync();
				var messageValue = true ? returnValue : "***REDACTED***";  // TODO: find environment value, set to false for prod
				_logger.LogTrace("Got token value {messageValue}, returning...", messageValue);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetClientTokenAsync");
			}
			_logger.LogTrace("Completing GetClientTokenAsync(HttpContext)");
			return returnValue;
		}


		public virtual async Task<string> GetTokenStringAsync(ApiAccessType accessType)
		{
			_logger.LogTrace("Starting GetTokenStringAsync(HttpContext)");
			_logger.LogTrace("GetTokenStringAsync(HttpContext) : AccessType {0}", accessType.ToString());
			var tokenString = string.Empty;
			try
			{
				switch (accessType)
				{
					case ApiAccessType.User:
						{
							var context = GetContextFromAccessor();
							tokenString = await this.GetUserTokenAsync(context);
							break;
						}
					case ApiAccessType.Client:
						{
							var context = GetContextFromAccessor();
							tokenString = await this.GetClientTokenAsync(context, _oidcOptions.ClientId);
							break;
						}
					case ApiAccessType.Idp:
						{
							throw new NotImplementedException("Not valid for this implementation.");
						}
					default:
						{
							break;
						}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetTokenStringAsync");
				throw;
			}
			_logger.LogTrace("Completing GetTokenStringAsync(HttpContext)");
			return tokenString;
		}

		public async Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType)
		{
			_logger.LogTrace("Starting GetDataFromApiAsync(url, accessType)");
			var returnValue = default(T);
			try
			{
				// TODO: use HttpClientFactory here, it's safer
				var token = await GetTokenStringAsync(accessType);
				returnValue = await this.GetDataFromApiAsync<T>(apiEndpointPartialUrl, token);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetDataFromApiAsync");
				throw;
			}
			_logger.LogTrace("Completing GetDataFromApiAsync(url, accessType)");
			return returnValue;
		}

		public async Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, string token)
		{
			_logger.LogTrace("Starting GetDataFromApiAsync(url, token)");
			var returnValue = default(T);
			try
			{
				// TODO: use HttpClientFactory here, it's safer
				var client = new HttpClient();
				_logger.LogTrace("GetDataFromApiAsync: Got token string");
				string authHeaderValue = $"Bearer {token}";
				client.DefaultRequestHeaders.Add("Authorization", authHeaderValue);
				client.BaseAddress = new Uri(_configuration.GetValue<string>("Api:BaseUrl"));
				var response = await client.GetAsync($"{apiEndpointPartialUrl}");
				_logger.LogDebug("GetDataFromApiAsync.response {0}", response);
				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.ReadAsStringAsync();
					_logger.LogDebug("GetDataFromApiAsync.responseData {0}", responseData);
					returnValue = JsonSerializer.Deserialize<T>(responseData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
				}
				else
				{
					throw new Exception($"InternalDataTransferService.GetDataFromApiAsync failed due to: {response.ReasonPhrase}");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetDataFromApiAsync(url, token)");
				throw;
			}
			_logger.LogTrace("Completing GetDataFromApiAsync(url, token)");
			return returnValue;
		}
	}
}
