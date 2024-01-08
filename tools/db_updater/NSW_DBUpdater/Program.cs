using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NSW.Info.Interfaces;
using Microsoft.Extensions.Configuration;

var builder =  Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddUserSecrets("d7df2e78-b68f-405a-821c-48eac048a5a8");

NSW.Info.Extensions.DependencyInjection.RegisterServices(builder.Services);

var host = builder.Build();



var serviceContainer = host.Services;
var _projectInfo = serviceContainer.GetRequiredService<IProjectInfo>();
var _appSettings = serviceContainer.GetRequiredService<IAppSettings>();
var _log = serviceContainer.GetRequiredService<ILog>();
var _configuration = serviceContainer.GetRequiredService<IConfiguration>();

// declare connections
SqlConnection LocalConnection = new SqlConnection(_configuration.GetConnectionString(_appSettings.GetAppSetting("ConnectionStringLocal", false)));
SqlConnection RemoteConnection = new SqlConnection(_configuration.GetConnectionString(_appSettings.GetAppSetting("ConnectionStringRemote", false)));
// declare data objects
SqlDataAdapter adap = new SqlDataAdapter();
DataSet rds = new DataSet();
try
{
	// fill remote data set
	string strSQL = "Select * from tblLabelText";
	adap = new SqlDataAdapter(strSQL, RemoteConnection);
	RemoteConnection.Open();
	adap.Fill(rds);
	RemoteConnection.Close();
	// check each row and update local db
	foreach (DataRow dr in rds.Tables[0].Rows)
	{
		string ID = dr["fldLabelText_ID"].ToString();
		string updateSQL = "UPDATE tblLabelText set fldLabelText_Japanese='" + dr["fldLabelText_Japanese"].ToString() + "' where fldLabelText_ID="+ID+";";
		SqlCommand ldc = new SqlCommand(updateSQL);
		ldc.Connection = LocalConnection;
		LocalConnection.Open();
		int result = ldc.ExecuteNonQuery();
		LocalConnection.Close();
	}
}
catch (Exception x)
{
	_log.WriteToLog(_projectInfo.ProjectLogType, "DBUpdater.Main", x, NSW.LogEnum.Critical);
}

await host.RunAsync();
