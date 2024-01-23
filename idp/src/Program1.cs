// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using NSW;
using NSW.Data.Extensions;
using NSW.Idp;
using NSW.Idp.Configuration;
using NSW.Idp.Data;
using NSW.Idp.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Text.Json;

bool EnvironmentRequiresSeedData(string environmentName)
{
    switch (environmentName)
    {
        case "Localhost":
        case "Development":
        case "QA":
        case "UAT":
        case "Stage":
        case "Staging":
            return true;
        default:
            return false;
    }
}

Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#endif
   .MinimumLevel.Override("NSW.Idp", Serilog.Events.LogEventLevel.Verbose)
   .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Debug)
   .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
   .Enrich.FromLogContext()
   .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext} {Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
#if DEBUG
   .WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}:: {Message:lj}{NewLine}{Exception}")
#endif
   .CreateLogger();

Log.Debug("Logger built.");

var builder = WebApplication.CreateBuilder(args);
// configure the logging
builder.Host.UseSerilog();

Log.Debug("Logging added to services.");
var environmentName = builder.Environment.EnvironmentName;

// load the configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
if(builder.Environment.EnvironmentName.Equals("localhost", StringComparison.InvariantCultureIgnoreCase))
{
    builder.Configuration.AddUserSecrets("4ccf36a0-933c-463f-a8aa-8b252c45c6b6", true);
}

Log.Debug("configuration built");

// load the kestrel config
builder.ConfigureNswKestrel();

Log.Debug("kestrel configured");

Log.Debug("starting configuring services....");

var oidcOptions = NSW.Data.Extensions.DependencyInjection.RegisterServices(builder.Services, builder.Configuration, DataTransferVaraintEnum.Tools);
NSW.Data.Extensions.DependencyInjection.RegisterPostalTask(builder.Services);
Log.Debug("NSW services added");

if (EnvironmentRequiresSeedData(builder.Environment.EnvironmentName))
{
    IdentityModelEventSource.ShowPII = true;
    Log.Debug("personally identifiable information allowed in logs");
}

builder.Services.AddControllersWithViews();
Log.Debug("controllers added");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(builder.Configuration.GetSection("ConnectionString").Value)));
Log.Debug("database context added");

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
Log.Debug("identity added");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(
            "http://localhost",
            "https://localhost",
            "http://bff:5004",
            "https://bff:5005",
            "http://api:5002",
            "https://api:5003",
            "http://idp:5006",
            "https://idp:5007",
            "https://localhost:3000",
            "http://localhost:3000",
            "http://localhost:5004",
            "https://localhost:5005"
            )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
Log.Debug("cors service added");

builder.Services.AddTransient<IdentityServer4.Services.ICorsPolicyService, CorsPolicyService>();
Log.Debug("identity server cors service added");

var options = new IdentityServerOptions
{
    IssuerUri = oidcOptions.Authority,
    Events = new EventsOptions
    {
        RaiseErrorEvents = true,
        RaiseInformationEvents = true,
        RaiseFailureEvents = true,
        RaiseSuccessEvents = true,
    },
    EmitStaticAudienceClaim = true,
};
builder.Services.AddSingleton<IdentityServerOptions>(options);
Log.Debug("identity server options added");

var idSrvBuilder = builder.Services.AddIdentityServer(options => { })
     .AddInMemoryIdentityResources(Config.IdentityResources)
     .AddInMemoryApiScopes(Config.ApiScopes)
     .AddInMemoryClients(Config.Clients)
     .AddInMemoryApiResources(Config.ApiResources)
     .AddAspNetIdentity<ApplicationUser>();
Log.Debug("identity server added");

// not recommended for production - you need to store your key material somewhere secure
idSrvBuilder.AddDeveloperSigningCredential();
Log.Debug("signing creds added");

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        // register your IdentityServer with Google at https://console.developers.google.com
        // enable the Google+ API
        // set the redirect URI to https://localhost:5001/signin-google
        options.ClientId = "copy client ID from Google here";
        options.ClientSecret = "copy client secret from Google here";
    });
Log.Debug("authentication added");

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
Log.Debug("completing configuring services....");
Log.Debug("building app");

var app = builder.Build();

#if DEBUG
foreach(var item in app.Configuration.AsEnumerable())
{
    Log.Debug(JsonSerializer.Serialize(item));
}
#endif

Log.Debug("Starting Configuring Pipeline");
if (app == null) throw new ArgumentNullException(nameof(app));
if (EnvironmentRequiresSeedData(environmentName))
{
    var connStringName = app.Configuration.GetSection("ConnectionString").Value;
    Log.Debug("ConnectionString Name: {connStringName}", connStringName);
    var connectionString = app.Configuration.GetConnectionString(connStringName);
    Log.Debug("ConnectionString: {connectionString}", connectionString);
    SeedData.EnsureSeedData(connectionString);
}
app.UseForwardedHeaders();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseIdentityServer();
app.UseAuthorization();

app.MapDefaultControllerRoute();

Log.Debug("Completing Configuring Pipeline");