using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace NSW.Bff.Internal
{
    /// <summary>
    /// captures access_denied and handles it gracefully.
    /// </summary>
    public class NswIdpAccessDeniedMiddleware 
    {
        private readonly ILogger<NswIdpAccessDeniedMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        /// <summary>
        /// the ctor.
        /// </summary>
        /// <param name="next">the delegate.</param>
        /// <param name="logger">the logger.</param>
        /// <param name="configuration">the appsettings.</param>
        public NswIdpAccessDeniedMiddleware(
            RequestDelegate next,
            ILogger<NswIdpAccessDeniedMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// created to capture a specific error from the IDP
        /// </summary>
        /// <param name="context">the httpContext.</param>
        /// <returns>nothing.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var newCopyOfExceptionForInspectionInDevelopment = ex;
                if (ex.InnerException.GetType() == typeof(Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectProtocolException))
                {
                    // the user likely clicked cancel on the login page, and we'll get and error here if we don't handle it.
                    // redirect the user to the access denied UI page.
                    var uiUrl = _configuration.GetValue<string>("Frontend:BaseUrl");
                    _logger.LogDebug(newCopyOfExceptionForInspectionInDevelopment, "User clicked cancel...");
                    context.Response.Redirect($"{uiUrl}/denied");
                }
            }
        }
    }
}
