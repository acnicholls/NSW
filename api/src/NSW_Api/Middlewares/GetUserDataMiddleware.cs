using Microsoft.AspNetCore.Authentication;
// above are guesses for token management.
using System.Security.Claims;
using NSW.Info.Interfaces;
using NSW.Services.Interfaces;

namespace NSW.Api.Middlewares
{
    public class GetUserDataMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly ILog _logger;

        public GetUserDataMiddleWare(
            RequestDelegate next,
            IUserService userService,
            ILog logger)
        {
            this._next = next;
            this._userService = userService;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogUserClaims(context);
            if (context?.User?.Identity?.IsAuthenticated ?? false)
            {
                LogUserToken(context);
                // call the db and get this user's data.  set thier claims
                var userId = context?.User?.Claims?.FirstOrDefault(x => x.Type == "sub")?.Value;
                if (userId != null)
                {
                    var thisUser = this._userService.GetById(Convert.ToInt32(userId));
                    if (thisUser != null)
                    {
                        var langPrefClaim = new Claim("language_preference", thisUser.LanguagePreference.ToString());
                        context.User.Claims.Append(langPrefClaim);
                    }
                }
            }
            await _next(context);
        }

        private void LogUserClaims(HttpContext context)
        {
            if (context?.User?.Claims?.Any() ?? false)
            {
                foreach (var claim in context.User.Claims.ToList())
                {
                    _logger.WriteToLog(LogTypeEnum.File, "GetUserDataMiddleware.InvokeAsync", "Type: " + claim.Type.ToString() + "  Value: " + claim.Value, LogEnum.Debug);
                }
            }
        }

        private async void LogUserToken(HttpContext context)
        {
            var token = await context.GetUserAccessTokenAsync();
            _logger.WriteToLog(LogTypeEnum.File, "GetUserDataMiddleware.LogUserToken", token, LogEnum.Debug);
        }
    }
}