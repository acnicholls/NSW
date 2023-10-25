using Microsoft.Data.SqlClient;
using NSW.Interfaces;
using System.Data;

namespace NSW.Repositories
{
	public class BaseRepository
	{
		protected readonly IUser _currentUser;

		private static readonly SqlConnection connection = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);

		public BaseRepository(IUser currentUser)
		{
			_currentUser = currentUser;
		}

		protected DataSet GetDataFromStoreProcedure(string procedureName)
		{
			DataSet ds = new DataSet();
			SqlCommand command = connection.CreateCommand();
			SqlDataAdapter adap = new SqlDataAdapter(command);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = procedureName;
			connection.Open();
			adap.Fill(ds);
			connection.Close();
			return ds;
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
			throw new NotImplementedException();
			//DataSet ds = new DataSet();
			//SqlCommand command = connection.CreateCommand();
			//SqlDataAdapter adap = new SqlDataAdapter(command);
			//command.CommandType = CommandType.Text;
			//command.CommandText = sqlString;
			//connection.Open();
			//adap.Fill(ds);
			//connection.Close();
			//return ds;
		}
		protected async Task<DataSet> GetDataFromStoreProcedureAsync(string procedureName)
		{
			throw new NotImplementedException();
			//DataSet ds = new DataSet();
			//SqlCommand command = connection.CreateCommand();
			//SqlDataAdapter adap = new SqlDataAdapter(command);
			//command.CommandType = CommandType.StoredProcedure;
			//command.CommandText = procedureName;
			//connection.Open();
			//adap.Fill(ds);
			//connection.Close();
			//return ds;
		}

		protected async Task ExecuteStoreProcedureAsync(string procedureName)
		{
			throw new NotImplementedException();
			//SqlCommand command = connection.CreateCommand();
			//command.CommandType = CommandType.StoredProcedure;
			//command.CommandText = procedureName;
			//connection.Open();
			//command.ExecuteNonQuery();
			//connection.Close();
		}

	}
}
