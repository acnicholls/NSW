using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info;
using NSW.Info.Interfaces;
using System.Data;

namespace NSW.Repositories
{
	public class BaseRepository
	{
		protected readonly IUser _currentUser = new User("test", "Test@acnicholls.com");
		protected readonly ILog _log;
		protected readonly IProjectInfo _projectInfo;
		protected readonly IConnectionInfo _connectionInfo;

		protected SqlConnection _connection { get; private set; }

		public BaseRepository(
			ILog log,
			IUser currentUser,
			IProjectInfo projectInfo,
			IConnectionInfo connectionInfo)
		{
			_currentUser = currentUser;
			_log = log;
			_projectInfo = projectInfo;
			_connectionInfo = connectionInfo;
			_connection = new SqlConnection(connectionInfo.ConnectionString);
		}

		protected int ExecuteStoreProcedure(string procedureName, List<SqlParameter> parameters)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = procedureName;
			foreach(SqlParameter p in parameters)
			{
				command.Parameters.Add(p);
			}
			_connection.Open();
			int returnValue = command.ExecuteNonQuery();
			_connection.Close();
			return returnValue;
		}

		protected DataSet GetDataFromSqlString(string sqlString)
		{
			try
			{
				DataSet ds = new DataSet();
				SqlCommand command = _connection.CreateCommand();
				SqlDataAdapter adap = new SqlDataAdapter(command);
				command.CommandType = CommandType.Text;
				command.CommandText = sqlString;
				_connection.Open();
				adap.Fill(ds);
				_connection.Close();
				return ds;
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "BaseRepository.GetDataFromSqlString", x, LogEnum.Critical);
				throw;
			}
		}

		protected int ExecuteNonQuery(string sqlString)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			_connection.Open();
			int returnValue = command.ExecuteNonQuery();
			_connection.Close();
			return returnValue;
		}

		protected async Task<DataSet> GetDataFromSqlStringAsync(string sqlString)
		{
			DataSet ds = new DataSet();
			SqlCommand command = _connection.CreateCommand();
			SqlDataAdapter adap = new SqlDataAdapter(command);
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			await _connection.OpenAsync();
			adap.Fill(ds);
			await _connection.CloseAsync();
			return ds;
		}

		protected async Task<int> ExecuteStoreProcedureAsync(string procedureName, List<SqlParameter> parameters)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = procedureName;
			foreach (SqlParameter p in parameters)
			{
				command.Parameters.Add(p);
			}
			await _connection.OpenAsync();
			int returnValue = await command.ExecuteNonQueryAsync();
			await _connection.CloseAsync();
			return returnValue;
		}

		protected async Task<int> ExecuteNonQueryAsync(string sqlString)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			await _connection.OpenAsync();
			int returnValue = await command.ExecuteNonQueryAsync();
			await _connection.CloseAsync();
			return returnValue;
		}

		protected string GetLabelTextFromDataRow(DataRow row)
		{
			switch (_currentUser.DisplayLanguage)
			{
				case LanguagePreference.English:
					{
						return row["fldLabel_English"].ToString();
					}
				case LanguagePreference.Japanese:
				default:
					{
						return row["fldLabel_Japanese"].ToString();
					}
			}
		}
	}
}
