using System;
using IdentityServer4.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace NSW.Idp.Configuration
{

    public class CorsPolicyService : IdentityServer4.Services.ICorsPolicyService
    {
        private readonly ILogger<CorsPolicyService> _logger;

        public CorsPolicyService(ILogger<CorsPolicyService> logger)
        {
            this._logger = logger;
        }
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            _logger.LogDebug("Attempting cors check for {0}.", origin);
            var allowedOrigins = new List<string>
                {
                    "http://api:5002",
                    "https://api:5003",
                    "http://bff:5004",
                    "https://bff:5005",
                    "http://idp:5006",
                    "https://idp:5007",
                    "http://localhost",
                    "https://localhost",
                    "http://localhost:5002",
                    "http://localhost:5004",
                    "http://localhost:5006",
					"https://localhost:5003",
					"https://localhost:5005",
					"https://localhost:5007",
				};

            if (allowedOrigins.Contains(origin))
            {
                _logger.LogDebug("Returning True");
                return Task.FromResult(true);
            }
            _logger.LogDebug("Returning False");
            return Task.FromResult(false);

        }
    }

}