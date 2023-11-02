// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Linq;

namespace Starter.Idp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
               .MinimumLevel.Override("System", LogEventLevel.Debug)
               .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
               // #if DEBUG                
               //                 .MinimumLevel.Override("Starter.Idp", LogEventLevel.)
               // #else
               //                 .MinimumLevel.Override("Starter.Idp", LogEventLevel.Warning)
               // #endif                
               .Enrich.FromLogContext()
               // uncomment to write to Azure diagnostics stream
               //.WriteTo.File(
               //    @"D:\home\LogFiles\Application\identityserver.txt",
               //    fileSizeLimitBytes: 1_000_000,
               //    rollOnFileSizeLimit: true,
               //    shared: true,
               //    flushToDiskInterval: TimeSpan.FromSeconds(1))
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
               .WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
               .CreateLogger();

            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }

                var host = CreateHostBuilder(args).Build();
                // host.Services.AddTransient<ILogger, Log>();

                if (seed)
                {
                    Log.Information("Seeding database...");
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var connectionString = config.GetConnectionString("DefaultConnection");
                    SeedData.EnsureSeedData(connectionString);
                    Log.Information("Done seeding database.");
                    return 0;
                }

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(opts =>
                    {
                        var address = System.Net.IPAddress.Parse("0.0.0.0");
                        opts.Listen(address, 5006);
                        opts.Listen(address, 5007, opts =>
                            opts.UseHttps(
                                "/ssl/idsrv-dotnet-react.pfx",
                                "123456"
                            ));
                    });
                });
    }
}