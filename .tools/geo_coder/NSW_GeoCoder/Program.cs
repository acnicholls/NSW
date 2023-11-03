using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NSW.Info.Interfaces;
using Microsoft.Extensions.Configuration;
using NSW.GeoCoder.Interfaces;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddUserSecrets("d7df2e78-b68f-405a-821c-48eac048a5a8");

NSW.Info.Extensions.DependencyInjection.RegisterServices(builder.Services);
NSW.GeoCoder.Extensions.DependencyInjection.RegisterServices(builder.Services);

var host = builder.Build();



var serviceContainer = host.Services;

var database = serviceContainer.GetRequiredService<IDatabase>();

database.ClearDatabaseFkConstraint();
database.AddNewPostalCodes();
database.ModifyTblUsersPostalCodes();
database.ReAddFKConstraintOnTblUsers();












