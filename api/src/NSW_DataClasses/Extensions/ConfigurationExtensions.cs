using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Serilog;

namespace NSW.Data.Extensions
{
    public static class ConfigurationExtensions
    {

        /// used in a couple places to list the configurations
        public static void ListConfiguration(this IConfiguration configuration)
        {
            Log.Debug("Listing Configuration...");
            foreach (var item in configuration.AsEnumerable())
            {
                Log.Debug(JsonSerializer.Serialize(item));
            }
            Log.Debug("Configuration Listed...");
        }

    }
}