using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSW.Data.Validation.Interfaces;
using Serilog;
using Serilog.Events;
using System;

namespace NSW.Bff
{
	public class Program
	{
		public static int Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("NSW", Serilog.Events.LogEventLevel.Verbose)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}:: {Message:lj}{NewLine}{Exception}")
				.CreateLogger();

			try
			{
				Log.Information("Starting NSW.Bff");
				var host = CreateHostBuilder(args).Build();


				host.Run();
				return 0;
			}
			catch (Exception x)
			{
				Log.Fatal(x, "Host terminated Unexpectedly...:(");
				return 1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.ConfigureWebHostDefaults(WebHostBuilder =>
				{
					WebHostBuilder.UseStartup<Startup>();
					// next portion put into release build for docker.  
					// but could likely be removed and env used for config.
#if !DEBUG
                    WebHostBuilder.UseKestrel(opts =>
                    {
                        var address = System.Net.IPAddress.Parse("0.0.0.0");
                        opts.Listen(address, 5004);
                        opts.Listen(address, 5005, opts => 
                            opts.UseHttps(
                                "/ssl/NSW_BFF.pfx",
                                "123456"
                        ));
                    });
#endif
				});

	}
}
