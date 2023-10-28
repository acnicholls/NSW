var builder = WebApplication.CreateBuilder(args);

// load the configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddUserSecrets("d7df2e78-b68f-405a-821c-48eac048a5a8", true);

builder.Services.AddHttpContextAccessor();
// Add services to the container.
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
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
