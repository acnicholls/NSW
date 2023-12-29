using BFF.Internal;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSW.Bff.Controllers
{
	[ApiController]
	[Route("bff/[controller]")]
	[Authorize]
	public class UserController : NswControllerBase
	{

		public UserController(
			IHttpContextAccessor httpContextAccessor,
			IDiscoveryCache discoveryCache,
			ILogger<UserController> logger,
			IConfiguration configuration,
	OidcOptions oidcOptions
		) : base(httpContextAccessor, discoveryCache, logger, configuration, oidcOptions) { }
		


		[HttpGet("info")]
		public async Task<IActionResult> GetUserAsync()
		{
			try
			{
				var disco = await base.GetIdpDiscoveryDocumentAsync();
				var token = await base.GetUserTokenAsync();


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
					Id = Convert.ToInt32(idpResponse.Claims.FirstOrDefault(x => x.Type == "sub")?.Value),
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