using Microsoft.IdentityModel.Tokens;
using NSW.Data.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("NSW", Serilog.Events.LogEventLevel.Verbose)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}:: {Message:lj}{NewLine}{Exception}")
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// configure the logging
builder.Host.UseSerilog();

// load the configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
//builder.Configuration.AddUserSecrets("d7df2e78-b68f-405a-821c-48eac048a5a8", true);
builder.Configuration.AddEnvironmentVariables();

// configure local reverse proxy application 
builder.ConfigureNswKestrel();

// Add services to the container.
builder.Services.AddHttpContextAccessor();
NSW.Info.Extensions.DependencyInjection.RegisterServices(builder.Services);
NSW.Data.Extensions.DependencyInjection.RegisterTestUser(builder.Services);
var oidcOptions = NSW.Data.Extensions.DependencyInjection.RegisterOidcOptions(builder.Services, builder.Configuration);
NSW.Repositories.Extensions.DependencyInjection.RegisterServices(builder.Services);
NSW.Services.Extensions.DependencyInjection.RegisterServices(builder.Services);

// add authentication
builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		// TODO: make these configuration options, so they can change per env
		options.Authority = oidcOptions.Authority;
		options.MetadataAddress = oidcOptions.MetadataAddress;
		options.RequireHttpsMetadata = oidcOptions.RequireHttpsMetadata;

		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false, // for development
			ValidateAudience = false  // for development
		};
	});

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.WithOrigins(
			"http://localhost",
			"https://localhost",
			"http://bff:5004",
			"https://bff:5005",
			"http://idp:5006",
			"https://idp:5007",
			"http://localhost:3000",
			"https://localhost:3000",
			"http://localhost:5004",
			"https://localhost:5005"
			)
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials());
});

builder.Services.AddControllers();

// only show a swagger page if we're in a development or staging environment.
if (builder.Environment.IsDevelopment() | builder.Environment.IsStaging())
{
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen((options) =>
	{
		//var fileLocation = $"{Environment.CurrentDirectory}\\NSW_Api.xml";
		//options.IncludeXmlComments(fileLocation);
	});
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
	app.Run();
	Log.Information("Host terminated gracefully.");
}
catch (Exception ex)
{
	Log.Fatal(ex, "host terminated unexpectedly...");
}
finally
{
	Log.CloseAndFlush();
}
