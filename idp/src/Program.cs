// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSW.Data;
using NSW.Data.Extensions;
using NSW.Data.Validation.Interfaces;
using NSW.Data.Internal.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace NSW.Idp
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("NSW", Serilog.Events.LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
               .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
               // #if DEBUG                
               //                 .MinimumLevel.Override("NSW.Idp", LogEventLevel.)
               // #else
               //                 .MinimumLevel.Override("NSW.Idp", LogEventLevel.Warning)
               // #endif                
               .Enrich.FromLogContext()
               // uncomment to write to Azure diagnostics stream
               //.WriteTo.File(
               //    @"D:\home\LogFiles\Application\identityserver.txt",
               //    fileSizeLimitBytes: 1_000_000,
               //    rollOnFileSizeLimit: true,
               //    shared: true,
               //    flushToDiskInterval: TimeSpan.FromSeconds(1))
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext} {Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
               .WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}:: {Message:lj}{NewLine}{Exception}")
               .CreateLogger();

            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }

                var host = CreateHostBuilder(args).Build();


                var postalCodeTask = host.Services.GetRequiredService<IPostalCodeTask>();
                postalCodeTask.StartBackgroundPostalCodeWorker(ApiAccessType.Idp);

                if (seed)
                {
                    Log.Information("Seeding database...");
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var connectionString = config.GetConnectionString(config.GetSection("ConnectionString").Value);
                    SeedData.EnsureSeedData(connectionString);
                    Log.Information("Done seeding database.");
                    return 0;
                }

                Log.Information("Starting host...");

                await host.RunAsync();
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
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    // configHost.AddJsonFile("hostsettings.json", optional: true);  // this isn't needed, just to prove a point
                    configHost.AddUserSecrets("4ccf36a0-933c-463f-a8aa-8b252c45c6b6");
                    configHost.AddEnvironmentVariables();
                    
                })
            .ConfigureNswKestrel()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}