﻿using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;
using System.Text.Json;
using IdentityServer4;
using System.Security.Claims;


namespace NSW.Data.Internal
{
	public class InternalDataTransferService : IInternalDataTransferService
	{
		private readonly IdentityServerTools _tools;
		private readonly ILogger<InternalDataTransferService> _logger;

		private readonly IDiscoveryCache _discoveryCache;

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;
		private readonly OidcOptions _oidcOptions;

		public InternalDataTransferService(
			IdentityServerTools tools,
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
			_tools = tools;
		}
		private DiscoveryDocumentResponse _discoveryDocument;

		public async Task<DiscoveryDocumentResponse> GetIdpDiscoveryDocumentAsync()
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
				_logger.LogError(_discoveryDocument.Exception, "InternalDataTransferService.GetIdpDiscoveryCacheAsync");
				throw new System.Exception("error getting discovery cache.");
			}


			_logger.LogDebug("DisocveryDocumentResponse {0}", _discoveryDocument.TokenEndpoint);
			_logger.LogDebug("DisocveryDocumentResponse {0}", _discoveryDocument.UserInfoEndpoint);
			return _discoveryDocument;
		}

		public async Task<string> GetUserTokenAsync()
		{
			var returnValue = string.Empty;
			try
			{
				returnValue = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetUserTokenAsync");
			}

			return returnValue;
		}

		public async Task<string> GetClientTokenAsync(string clientId = "default")
		{
			var returnValue = string.Empty;
			try
			{
				returnValue = await _httpContextAccessor.HttpContext.GetClientAccessTokenAsync(clientId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "InternalDataTransferService.GetClientTokenAsync");
			}

			return returnValue;
		}

		private async Task<string> GetTokenStringAsync(ApiAccessType accessType)
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
							// tokenString = await _tools.IssueClientJwtAsync("NSW.Bff", 600, new[] { "NSW.ApiScope", "openid", "profile" }, new[] { "NSW.Api" });
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

		public async Task<T> GetDataFromApiAsync<T>(string apiEndpointPartialUrl, ApiAccessType accessType)
		{
			var returnValue = default(T);
			try
			{
				// TODO: use HttpClientFactory here, it's safer
				var client = new HttpClient();
				var token = await GetTokenStringAsync(accessType);
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
				_logger.LogError(ex, "InternalDataTransferService.GetDataFromApiAsync");
				throw;
			}
			return returnValue;
		}
	}
}