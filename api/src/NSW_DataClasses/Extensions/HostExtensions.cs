using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


namespace NSW.Data.Extensions
{
    public static class HostExtensions
    {
        public static WebApplicationBuilder ConfigureNswKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            ConfigureNswKestrel(serverOptions));

            return builder;
        }

        public static IHostBuilder ConfigureNswKestrel(this IHostBuilder builder)
        {                   
            builder.ConfigureWebHost(hostBuilder =>
            {
                hostBuilder.ConfigureKestrel(serverOptions=> ConfigureNswKestrel(serverOptions));
            });

            return builder;
        }

        private static KestrelServerOptions ConfigureNswKestrel(KestrelServerOptions serverOptions)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            // get the location of the files from the configuration
            IConfigurationBuilder configBuilder = new
                ConfigurationBuilder()
                .SetBasePath(System.Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true);
            IConfigurationRoot config = configBuilder.Build();

            serverOptions.ConfigureHttpsDefaults(listenOptions =>
            {


                // create a new key in memory
                using (var privateKey = RSA.Create())
                {
                    // load our key value
                    string keyLocation = config.GetValue<string>("Ssl:keyLocation") ?? "/ssl/api.key";
                    privateKey.ImportPkcs8PrivateKey(PemBytes(keyLocation), out var bytesRead);
                    // read our certificate file
                    string certLocation = config.GetValue<string>("Ssl:certLocation") ?? "/ssl/api.crt";
                    X509Certificate2 certFile = new(certLocation);
                    // combine the certificate and key into a ServerCertificate
                    listenOptions.ServerCertificate = certFile.CopyWithPrivateKey(privateKey);
                }
            });
            // custom server options
            var address = System.Net.IPAddress.Any;
            serverOptions.Listen(address, config.GetValue<int>("Ssl:httpPort"));
            serverOptions.Listen(address, config.GetValue<int>("Ssl:httpsPort"), options => options.UseHttps());
            return serverOptions;
        }



        /// <summary>
        /// this method converts the Base64 content of the key file to a machine readable RSA private key.
        /// </summary>
        /// <param name="fileName">the location of the key</param>
        /// <returns>a byte array.</returns>
        public static byte[] PemBytes(string fileName) =>
            Convert.FromBase64String(
                File.ReadAllLines(fileName)
                .Where(l => !l.Contains('-'))
                .Aggregate("", (current, next) => current + next));
    }
}
