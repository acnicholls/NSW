using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace NSW.Bff.Controllers
{
	[ApiController]
	[Route("bff/[controller]")]
	[Authorize]
	public class UserController : ControllerBase
	{

		private readonly ILogger<UserController> _logger;

		private readonly IDiscoveryCache _discoveryCache;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserController(
			IHttpContextAccessor httpContextAccessor,
			IDiscoveryCache discoveryCache,
			ILogger<UserController> logger
		)
		{
			_logger = logger;
			_discoveryCache = discoveryCache;
			_httpContextAccessor = httpContextAccessor;
		}


		[HttpGet("info")]
		public async Task<IActionResult> GetUserAsync()
		{
			try
			{
				DiscoveryDocumentResponse disco = await _discoveryCache.GetAsync();

				_logger.LogDebug("DisocveryDocumentResponse.IsError {0}", disco.IsError);
				_logger.LogDebug("DisocveryDocumentResponse.Error {0}", disco.Error);
				_logger.LogDebug("DisocveryDocumentResponse.ErrorType {0}", disco.ErrorType);
				_logger.LogDebug("DisocveryDocumentResponse.Exception {0}", disco.Exception);

				_logger.LogDebug("DisocveryDocumentResponse.HttpErrorReason {0}", disco.HttpErrorReason);
				_logger.LogDebug("DisocveryDocumentResponse.HttpStatusCode {0}", disco.HttpStatusCode);

				if (disco == null)
				{
					_logger.LogInformation("disco is null");
				}

				if (disco.IsError)
				{
					_logger.LogError(disco.Exception, "UserController.GetUserAsync Discovery");
					return this.StatusCode(500, "error in user controller");
				}

				_logger.LogDebug("DisocveryDocumentResponse {0}", disco.TokenEndpoint);
				_logger.LogDebug("DisocveryDocumentResponse {0}", disco.UserInfoEndpoint);

				var token = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync();

				if (token == null)
				{
					//_logger.LogError(tokenResponse.Exception, "UserController.GetUserAsync Token");
					return this.StatusCode(500, "error in user controller");
				}

				//var tokenEndpoint = disco.TokenEndpoint;

				var client = new HttpClient();
				//var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
				//{
				//    Address = disco.TokenEndpoint,
				//    ClientId = "NSW.Bff",
				//    ClientSecret = "secret",
				//    Scope = "profile"
				//});
				//_logger.LogDebug("TokenResponse.IsError {0}", tokenResponse.IsError);
				//_logger.LogDebug("TokenResponse.Error {0}", tokenResponse.Error);
				//_logger.LogDebug("TokenResponse.ErrorType {0}", tokenResponse.ErrorType);
				//_logger.LogDebug("TokenResponse.Exception {0}", tokenResponse.Exception);

				//_logger.LogDebug("TokenResponse.HttpErrorReason {0}", tokenResponse.HttpErrorReason);
				//_logger.LogDebug("TokenResponse.HttpStatusCode {0}", tokenResponse.HttpStatusCode);

				//if(tokenResponse.IsError)
				//{
				//    _logger.LogError(tokenResponse.Exception, "UserController.GetUserAsync Token");
				//    return this.StatusCode(500, "error in user controller");
				//}

				//_logger.LogDebug("TokenResponse.IdentityToken {0}", tokenResponse.IdentityToken);
				//_logger.LogDebug("TokenResponse.AccessToken {0}", tokenResponse.AccessToken);
				//_logger.LogDebug("TokenResponse.AccessToken {0}", tokenResponse.RefreshToken);

				var idpTask = GetUserInfoFromIDPAsync(token, disco.UserInfoEndpoint);

				var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value);
				var apiTask = GetUserInfoFromApi(userId, token);

				Task.WaitAll(idpTask, apiTask);

				_logger.LogDebug("user : {0}", JsonSerializer.Serialize(User.Identity));

				var idpResponse = idpTask.Result;
				var apiResponse = apiTask.Result;

				var user = new
				{
					cookiename = User.Identity.Name,
					Id = idpResponse.Claims.FirstOrDefault(x => x.Type == "sub")?.Value,
					Name = idpResponse.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "",
					Email = idpResponse.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? "",
					FirstName = idpResponse.Claims.FirstOrDefault(x => x.Type == "given_name")?.Value ?? "",
					LastName = idpResponse.Claims.FirstOrDefault(x => x.Type == "family_name")?.Value ?? "",
					Username = idpResponse.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? "",
					Role = apiResponse.Role ?? "Empty",
					LanguagePreference = apiResponse.LanguagePreference,
					IsAuthenticated = true,
				};

				return new JsonResult(user);
			}
			catch (Exception x)
			{
				_logger.LogError(x, "UserController.GetUserAsync");
				return this.StatusCode(500, "error in user controller");
			}
		}

		private async Task<UserInfoResponse> GetUserInfoFromIDPAsync(string token, string userInfoEndpoint)
		{
			var client = new HttpClient();
			var idpResponse = await client.GetUserInfoAsync(
				new UserInfoRequest
				{
					Address = userInfoEndpoint,
					Token = token  // tokenResponse.AccessToken
				});
			if (idpResponse.IsError)
			{
				_logger.LogDebug("UserInfoResponse.IsError {0}", idpResponse.IsError);
				_logger.LogDebug("UserInfoResponse.Error {0}", idpResponse.Error);
				_logger.LogDebug("UserInfoResponse.ErrorType {0}", idpResponse.ErrorType);
				_logger.LogDebug("UserInfoResponse.Exception {0}", idpResponse.Exception);

				_logger.LogDebug("UserInfoResponse.HttpErrorReason {0}", idpResponse.HttpErrorReason);
				_logger.LogDebug("UserInfoResponse.HttpStatusCode {0}", idpResponse.HttpStatusCode);
				throw new Exception("error getting user info response");
			}

			foreach (var claim in idpResponse.Claims)
			{
				_logger.LogDebug($"{claim.Type} : {claim.Value}");
			}
			return idpResponse;
		}

		private async Task<NSW.Data.User> GetUserInfoFromApi(int userId, string token)
		{
			var client = new HttpClient();
			string authHeaderValue = $"Bearer {token}";
			client.DefaultRequestHeaders.Add("Authorization", authHeaderValue);
			client.BaseAddress = new Uri("https://localhost:5003");
			var response = await client.GetAsync($"/api/User/{userId}");
			_logger.LogDebug("GetUserInfoFromApi.response {0}", response);
			if (response.IsSuccessStatusCode)
			{
				var userData = await response.Content.ReadAsStringAsync();
				_logger.LogDebug("GetUserInfoFromApi.userData {0}", userData);
				var userInfo = JsonSerializer.Deserialize<NSW.Data.User>(userData);
				return userInfo;
			}
			return AnonymousUser;
		}

		private NSW.Data.User AnonymousUser
		{
			get
			{
				return new NSW.Data.User
				{
					ID = -1,
					Name = "Anonymous User",
					Email = "Anonymous@anonym.ous",
					Role = "Empty",
					LanguagePreference = -1,
				};
			}
		}

		[Route("logout")]
		public IActionResult Logout()
		{
			return SignOut("cookies", "oidc");
		}
	}
}