using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info.Interfaces;
using System.Data;

namespace NSW.Repositories
{
	public class BaseRepository
	{
		protected readonly IUser _currentUser = new User("test", "Test@acnicholls.com");
		protected readonly ILog _log;
		protected readonly IProjectInfo _projectInfo;

		private static readonly SqlConnection connection = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);

		public BaseRepository(
			ILog log,
			IUser currentUser,
			IProjectInfo projectInfo)
		{
			_currentUser = currentUser;
			_log = log;
			_projectInfo = projectInfo;
		}

		protected int ExecuteStoreProcedure(string procedureName, List<SqlParameter> parameters)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = procedureName;
			foreach(SqlParameter p in parameters)
			{
				command.Parameters.Add(p);
			}
			connection.Open();
			int returnValue = command.ExecuteNonQuery();
			connection.Close();
			return returnValue;
		}

		protected DataSet GetDataFromSqlString(string sqlString)
		{
			DataSet ds = new DataSet();
			SqlCommand command = connection.CreateCommand();
			SqlDataAdapter adap = new SqlDataAdapter(command);
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			connection.Open();
			adap.Fill(ds);
			connection.Close();
			return ds;
		}

		protected int ExecuteNonQuery(string sqlString)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			connection.Open();
			int returnValue = command.ExecuteNonQuery();
			connection.Close();
			return returnValue;
		}

		protected async Task<DataSet> GetDataFromSqlStringAsync(string sqlString)
		{
			DataSet ds = new DataSet();
			SqlCommand command = connection.CreateCommand();
			SqlDataAdapter adap = new SqlDataAdapter(command);
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			await connection.OpenAsync();
			adap.Fill(ds);
			await connection.CloseAsync();
			return ds;
		}

		protected async Task<int> ExecuteStoreProcedureAsync(string procedureName, List<SqlParameter> parameters)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = procedureName;
			foreach (SqlParameter p in parameters)
			{
				command.Parameters.Add(p);
			}
			await connection.OpenAsync();
			int returnValue = await command.ExecuteNonQueryAsync();
			await connection.CloseAsync();
			return returnValue;
		}

		protected async Task<int> ExecuteNonQueryAsync(string sqlString)
		{
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = sqlString;
			await connection.OpenAsync();
			int returnValue = await command.ExecuteNonQueryAsync();
			await connection.CloseAsync();
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
