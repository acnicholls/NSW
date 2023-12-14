using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs\\NSW.Bff.log")
                .CreateLogger();

            try{
                Log.Information("Starting NSW.Bff");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch(Exception x)
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
                .ConfigureWebHostDefaults(WebHostBuilder => {
                    WebHostBuilder.UseStartup<Startup>();
					//next portion put into release build for docker.  
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
