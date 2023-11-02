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

namespace Starter.Bff.Controllers
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
                //    ClientId = "Starter.Bff",
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

                var response = await client.GetUserInfoAsync(
                    new UserInfoRequest
                    {
                        Address = disco.UserInfoEndpoint,
                        Token = token  // tokenResponse.AccessToken
                    });
                if (response.IsError)
                {
                    _logger.LogDebug("UserInfoResponse.IsError {0}", response.IsError);
                    _logger.LogDebug("UserInfoResponse.Error {0}", response.Error);
                    _logger.LogDebug("UserInfoResponse.ErrorType {0}", response.ErrorType);
                    _logger.LogDebug("UserInfoResponse.Exception {0}", response.Exception);

                    _logger.LogDebug("UserInfoResponse.HttpErrorReason {0}", response.HttpErrorReason);
                    _logger.LogDebug("UserInfoResponse.HttpStatusCode {0}", response.HttpStatusCode);
                    return this.StatusCode(500, "error getting user info response");
                }

                foreach(var claim in response.Claims)
                {
                    _logger.LogDebug($"{claim.Type} : {claim.Value}");
                }

                _logger.LogDebug("user : {0}", JsonSerializer.Serialize(User.Identity));

                var user = new
                {
                    cookiename = User.Identity.Name,
                    Id = response.Claims.FirstOrDefault(x => x.Type == "sub").Value,
                    Name = response.Claims.FirstOrDefault(x => x.Type == "name").Value,
                    FirstName = response.Claims.FirstOrDefault(x => x.Type == "given_name").Value,
                    LastName = response.Claims.FirstOrDefault(x => x.Type == "family_name").Value,
                    Username = response.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value,
                    Website = response.Claims.FirstOrDefault(x => x.Type == "website").Value,
                    IsAuthenticated = true,
                };

                return new JsonResult(user);
            }
            catch(Exception x)
            {
                _logger.LogError(x, "UserController.GetUserAsync");
                return this.StatusCode(500, "error in user controller");
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            return SignOut("cookies", "oidc");
        }
    }
}