using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Starter.Bff
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
                .WriteTo.File("starter.bff.log")
                .CreateLogger();

            try{
                Log.Information("Starting Starter.Bff");
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
                });
                
    }
}
