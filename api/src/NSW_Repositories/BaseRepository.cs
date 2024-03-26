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
		protected readonly IUser _currentUser;
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

		#region private methods
		private SqlDataAdapter GetAdapterFromConnection(string sqlString)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			SqlDataAdapter adap = new SqlDataAdapter(command);
			return adap;
		}

		private SqlCommand GetTextCommandFromConnection(string sqlString)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			return command;
		}

		private SqlCommand GetProcCommandFromConnection(string sqlString, List<SqlParameter> parameters)
		{
			SqlCommand command = _connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = sqlString;
			foreach (SqlParameter p in parameters)
			{
				command.Parameters.Add(p);
			}
			return command;
		}
		#endregion

		#region GetDataFromSqlString
		protected DataSet GetDataFromSqlString(string sqlString)
		{
			try
			{
				DataSet ds = new DataSet();
				SqlDataAdapter adap = GetAdapterFromConnection(sqlString);
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
		protected async Task<DataSet> GetDataFromSqlStringAsync(string sqlString) => await Task.Run(() => this.GetDataFromSqlString(sqlString));
		//protected async Task<DataSet> GetDataFromSqlStringAsync(string sqlString)
		//{
		//	DataSet ds = new DataSet();
		//          SqlDataAdapter adap = GetAdapterFromConnection(sqlString);
		//	await _connection.OpenAsync();
		//	adap.Fill(ds);
		//	await _connection.CloseAsync();
		//	return ds;
		//}
		#endregion GetDataFromSqlString

		#region StoredProcedures
		protected int ExecuteStoreProcedure(string procedureName, List<SqlParameter> parameters)
		{
			SqlCommand command = GetProcCommandFromConnection(procedureName, parameters);
			return ExecuteNonQuery(command);
		}
		protected async Task<int> ExecuteStoreProcedureAsync(string procedureName, List<SqlParameter> parameters) =>
			await Task.Run(() => this.ExecuteStoreProcedure(procedureName, parameters));
		#endregion StoredProcedures

		#region NonQuery
		protected async Task<int> ExecuteNonQueryAsync(string sqlString) => await Task.Run(() => this.ExecuteNonQuery(sqlString));

		protected int ExecuteNonQuery(string sqlString)
		{
			SqlCommand command = GetTextCommandFromConnection(sqlString);
			return ExecuteNonQuery(command);
		}

		private int ExecuteNonQuery(SqlCommand command)
		{
			_connection.Open();
			int returnValue = command.ExecuteNonQuery();
			_connection.Close();
			return returnValue;
		}
		#endregion NonQuery
		protected string GetLabelTextFromDataRow(DataRow row)
		{
			switch ((LanguagePreference)_currentUser.LanguagePreference)
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
