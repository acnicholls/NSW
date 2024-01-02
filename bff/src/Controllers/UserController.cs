using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data.DTO.Response;
using NSW.Data.Internal;
using NSW.Data.Internal.Interfaces;
using NSW.Data.Internal.Models;
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
	public class UserController : ControllerBase
	{

		private readonly IInternalDataTransferService _service;
		private readonly ILogger<UserController> _logger;

		public UserController(
			IInternalDataTransferService service,
			ILogger<UserController> logger
			)
		{
			_service = service;
			_logger = logger;
		}



		[HttpGet("info")]
		public async Task<IActionResult> GetUserAsync()
		{
			try
			{
				var idpTask = GetUserInfoFromIDPAsync();
				var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value);
				var apiTask = _service.GetDataFromApiAsync<UserResponse>($"/api/User/{userId}", ApiAccessType.User);

				Task.WaitAll(idpTask, apiTask);

				_logger.LogDebug("user : {0}", JsonSerializer.Serialize(User.Identity));

				var idpResponse = idpTask.Result;
				var apiResponse = apiTask.Result;

				var postalCodeResponse = apiResponse.PostalCode;

				var user = new UserResponse
				{
					Id = Convert.ToInt32(idpResponse.Claims.FirstOrDefault(x => x.Type == "sub")?.Value),
					UserName = idpResponse.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value ?? "",
					Email = idpResponse.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? "",
					Phone = idpResponse.Claims.FirstOrDefault(x => x.Type == "phone_number")?.Value ?? "",
					PostalCode = new PostalCodeResponse
					{
						Code = idpResponse.Claims.FirstOrDefault(x => x.Type == "postal_code")?.Value ?? "",
					},
					Role = idpResponse.Claims.FirstOrDefault(x => x.Type == "role")?.Value ?? "",
					LanguagePreference = (int)apiResponse.LanguagePreference,
					IsAuthenticated = true,
				};

				return new JsonResult(user);
			}
			catch (Exception x)
			{
				_logger.LogError(x, "UserController.GetUserAsync");
				return new JsonResult(GetAnonymousUser);
			}
		}

		private UserResponse GetAnonymousUser
		{
			get
			{
				var user = new UserResponse
				{
					Id = -1,
					Email = "Anon@centra.serv",
					Phone = "99999999",
					PostalCode = new PostalCodeResponse(),
					UserName = "Anonymous User",
					Role = "Empty",
					LanguagePreference = 1,
					IsAuthenticated = false,
				};
				return user;
			}
		}



		private async Task<UserInfoResponse> GetUserInfoFromIDPAsync()
		{
			var disco = await _service.GetIdpDiscoveryDocumentAsync();
			var token = await _service.GetUserTokenAsync();
			var client = new HttpClient();
			var idpResponse = await client.GetUserInfoAsync(
				new UserInfoRequest
				{
					Address = disco.UserInfoEndpoint,
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


		[Route("logout")]
		public IActionResult Logout()
		{
			return SignOut("cookies", "oidc");
		}
	}
}