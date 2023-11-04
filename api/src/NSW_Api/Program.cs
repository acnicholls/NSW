
using Microsoft.IdentityModel.Tokens;
using NSW.Api;

var builder = WebApplication.CreateBuilder(args);

// load the configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddUserSecrets("d7df2e78-b68f-405a-821c-48eac048a5a8", true);
builder.Configuration.AddEnvironmentVariables("NSW_");

// configure local reverse proxy application 
builder.ConfigureNswKestrel();

// add authentication
builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.Authority = "https://localhost";
		options.MetadataAddress = "http://idp:5006/.well-known/openid-configuration";
		options.RequireHttpsMetadata = false;

		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false  // for development
		};
	});


// Add services to the container.
builder.Services.AddHttpContextAccessor();
NSW.Info.Extensions.DependencyInjection.RegisterServices(builder.Services);
NSW.Data.Extensions.DependencyInjecction.RegisterServices(builder.Services);
NSW.Repositories.Extensions.DependencyInjection.RegisterServices(builder.Services);
NSW.Services.Extensions.DependencyInjection.RegisterServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) => 
{
	var fileLocation = $"{Environment.CurrentDirectory}\\bin\\Debug\\net7.0\\NSW_Api.xml";
	options.IncludeXmlComments(fileLocation);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
