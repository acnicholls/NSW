using System;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static IdentityModel.OidcConstants;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using BFF.Internal;

namespace NSW.Bff.Controllers
{
	public class NswControllerBase : ControllerBase
	{
		protected readonly ILogger<NswControllerBase> _logger;

		protected readonly IDiscoveryCache _discoveryCache;

		protected readonly IHttpContextAccessor _httpContextAccessor;
		protected readonly IConfiguration _configuration;
		private readonly OidcOptions _oidcOptions;

		public NswControllerBase(
	IHttpContextAccessor httpContextAccessor,
	IDiscoveryCache discoveryCache,
	ILogger<NswControllerBase> logger,
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
		private DiscoveryDocumentResponse _discoveryDocument;

		protected async Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync()
		{
			if (_discoveryDocument != null)
			{
				return _discoveryDocument;
			}
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
				_logger.LogError(_discoveryDocument.Exception, "NswControllerBase.GetIdpDiscoveryCacheAsync");
				throw new System.Exception("error getting discovery cache.");
			}


			_logger.LogDebug("DisocveryDocumentResponse {0}", _discoveryDocument.TokenEndpoint);
			_logger.LogDebug("DisocveryDocumentResponse {0}", _discoveryDocument.UserInfoEndpoint);
			return _discoveryDocument;
		}

		protected async Task<string> GetUserTokenAsync()
		{
			var returnValue = string.Empty;
			try
			{
				returnValue = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "NswControllerBase.GetUserTokenAsync");
			}

			return returnValue;
		}

		protected async Task<string> GetClientTokenAsync()
		{
			var returnValue = string.Empty;
			try
			{
				returnValue = await _httpContextAccessor.HttpContext.GetClientAccessTokenAsync(_oidcOptions.ClientId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "NswControllerBase.GetClientTokenAsync");
			}

			return returnValue;
		}

		protected async Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType)
		{
			var returnValue = default(T);
			try
			{
				// TODO: use HttpClientFactory here, it's safer
				var client = new HttpClient();
				var token = accessType == ApiAccessType.User ? await this.GetUserTokenAsync() : await this.GetClientTokenAsync();
				string authHeaderValue = $"Bearer {token}";
				client.DefaultRequestHeaders.Add("Authorization", authHeaderValue);
				client.BaseAddress = new Uri(_configuration.GetValue<string>("Api:ForwardTo"));
				var response = await client.GetAsync($"{apiEndpointPartialUrl}");
				_logger.LogDebug("GetDataFromApiAsync.response {0}", response);
				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.ReadAsStringAsync();
					_logger.LogDebug("GetDataFromApiAsync.responseData {0}", responseData);
					returnValue = JsonSerializer.Deserialize<T>(responseData, new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});
				}
				else
				{
					throw new Exception($"NswControllerBase.GetDataFromApiAsync failed due to: {response.ReasonPhrase}");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "NswControllerBase.GetDataFromApiAsync");
				throw;
			}
			return returnValue;
		}
	}
}
