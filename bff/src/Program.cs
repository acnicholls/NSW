using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSW.Data.Extensions;
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
            .ConfigureNswKestrel()
				.ConfigureWebHostDefaults(hostBuilder =>
				{
					hostBuilder.UseStartup<Startup>();
				});

	}
}
