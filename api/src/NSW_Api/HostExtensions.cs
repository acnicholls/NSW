using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace NSW.Api
{
	public static class HostExtensions
	{
		public static WebApplicationBuilder ConfigureNswKestrel(this WebApplicationBuilder builder)
		{
			builder.WebHost.ConfigureKestrel(serverOptions =>
			{
				serverOptions.ConfigureHttpsDefaults(listenOptions =>
				{
					// get the location of the files from the configuration
					IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(System.Environment.CurrentDirectory).AddJsonFile("appsettings.json", false, true);
					IConfigurationRoot config = builder.Build();

					// create a new key in memory
					using (var privateKey = RSA.Create())
					{
						// load our key value
						string keyLocation = config.GetSection("Ssl:keyLocation").Value;
						privateKey.ImportPkcs8PrivateKey(Encryption.PemBytes(keyLocation), out var bytesRead);
						// read our certificate file
						string certLocation = config.GetSection("Ssl:certLocation").Value;
						X509Certificate2 certFile = new(certLocation);
						// combine the certificate and key into a ServerCertificate
						listenOptions.ServerCertificate = certFile.CopyWithPrivateKey(privateKey);
					}
				});
				// custom server options
				var address = System.Net.IPAddress.Any;
				serverOptions.Listen(address, 5002);
				serverOptions.Listen(address, 5003, options => options.UseHttps());
			});

			return builder;
		}

	}
}
