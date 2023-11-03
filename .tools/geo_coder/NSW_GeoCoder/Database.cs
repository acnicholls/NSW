using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NSW.GeoCoder.Data;
using NSW.Info.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSW.GeoCoder.Interfaces;

namespace NSW.GeoCoder
{
	public class Database : IDatabase
	{
		/// <summary>
		/// this is the connection to the database
		/// </summary>
		private readonly SqlConnection sqlConn;
		private SqlCommand sqlComm;
		private readonly IConfiguration _configuration;
		private readonly IProjectInfo _projectInfo;
		private readonly ILog _log;
		private readonly IApiCall _apiCall;


		public Database(
			IConfiguration configuration,
			IProjectInfo projectInfo,
			ILog log,
			IApiCall apiCall
			) 
		{
			this._configuration = configuration;
			_projectInfo = projectInfo;
			_log = log;
			_apiCall = apiCall;
			sqlConn = new SqlConnection(_configuration.GetConnectionString(_configuration.GetSection("ConnectionString").Value));
		}

		/// <summary>
		/// checks to see if the postal code passed in is already in the database
		/// </summary>
		/// <param name="code">postal code to check for</param>
		/// <returns>true/false</returns>
		private bool CheckForDuplicate(string code)
		{
			try
			{
				SqlCommand nswComm = sqlConn.CreateCommand();
				nswComm.CommandType = CommandType.Text;
				nswComm.CommandText = "Select fldPostal_Code from tblPostalCodes where fldPostal_code='" + code + "'";
				// declare SQL parameters
				// now send that to the db
				if (sqlConn.State == ConnectionState.Closed)
					sqlConn.Open();
				object result = nswComm.ExecuteScalar();
				sqlConn.Close();
				if (result != null)
				{
					string foundCode = result.ToString();
					if (foundCode == code)
						return true;
				}
				else
					return false;
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "Main", x, LogEnum.Critical);
				if (sqlConn.State == ConnectionState.Open)
					sqlConn.Close();
			}
			return true;
		}

		public void ClearDatabaseFkConstraint()
		{
			#region first remove the FK constraint on the users table
			_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Removing Constraint...", LogEnum.Message);
			sqlComm = sqlConn.CreateCommand();
			sqlComm.CommandType = CommandType.Text;
			string sqlString = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsers_tblPostalCodes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))";
			sqlString += "  ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [FK_tblUsers_tblPostalCodes]";
			sqlComm.CommandText = sqlString;
			sqlConn.Open();
			sqlComm.ExecuteNonQuery();
			sqlConn.Close();
			_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Constraint Removed!", LogEnum.Message);
			#endregion

		}

		public void AddNewPostalCodes()
		{
			#region now add all the new postal codes.
			// load the file 
			string fileLocation = Environment.CurrentDirectory.ToString() + @"\postalcodes.txt";
			StreamReader sr = new StreamReader(fileLocation);
			string line = "";
			string code = "";
			// first we pull ALL the zones into the database from the text file line by line
			Console.WriteLine("Press enter if you're ready to read file?");
			Console.Read();
			// read the file line by line
			while (sr.Peek() > -1)
			{
				line = sr.ReadLine().Trim();
				_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "checking for : " + line, LogEnum.Message);
				code = line.Remove(3, 1);
				// check for duplicate postal code with the hyphen in the postal code
				bool with = CheckForDuplicate(line);
				_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "with :" + with.ToString(), LogEnum.Debug);
				// check for duplicate postal code without the hyphen
				bool without = CheckForDuplicate(code);
				_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "without :" + without.ToString(), LogEnum.Debug);
				// if it doesn't exist in the database at all
				if (!with & !without)
				{
					#region add to db
					// now take those values and put together a google maps api call to find a lat/long
					coords postalCoords = _apiCall.APICALL(code);
					if (postalCoords != null)
					{
						_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Coords found, inserting new code : " + line, LogEnum.Message);
						//Console.WriteLine("Latitude : {0}", postalCoords.latitude);
						//Console.WriteLine("Longitude : {0}", postalCoords.longitude);
						SqlCommand nswComm = sqlConn.CreateCommand();
						nswComm.CommandType = CommandType.StoredProcedure;
						nswComm.CommandText = "insertPostalCode";
						// declare SQL parameters
						SqlParameter param = new SqlParameter("@code", line);
						nswComm.Parameters.Add(param);
						param = new SqlParameter("@lat", postalCoords.latitude);
						nswComm.Parameters.Add(param);
						param = new SqlParameter("@long", postalCoords.longitude);
						nswComm.Parameters.Add(param);
						// now send that to the db
						if (sqlConn.State == ConnectionState.Closed)
							sqlConn.Open();
						int result = nswComm.ExecuteNonQuery();
						sqlConn.Close();
						if (result == 1)
							Console.WriteLine("Postal Code Inserted : {0}", code);
						else
						{
							Console.WriteLine("Error Encountered");
							_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Error encountered Inserting new code with coords", LogEnum.Error);
						}
					}
					else
					{
						Console.WriteLine("Error Encountered");
						_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Error getting coords from google", LogEnum.Error);
					}
					#endregion
				}
				else
				{
					if (without)
					{
						Console.WriteLine("Found duplicate........................updating.");
						SqlCommand updComm = sqlConn.CreateCommand();
						updComm.CommandType = CommandType.Text;
						updComm.CommandText = "Update tblPostalCodes set fldPostal_Code = '" + line + "' where fldPostal_Code='" + code + "'";
						sqlConn.Open();
						int result = updComm.ExecuteNonQuery();
						sqlConn.Close();
						_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "update duplicate result:" + result.ToString(), LogEnum.Message);
						if (result != 1)
						{
							Console.WriteLine("Error updating duplicate....");
							Exception x = new Exception("Error updating duplicate..." + code);
							throw x;
						}
					}
					if (with)
					{
						Console.WriteLine("Found duplicate........................continuing.");
						_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "found duplicate :" + line, LogEnum.Message);
					}
				}
			}
			sr.Close();
			#endregion

		}
	
		public void ModifyTblUsersPostalCodes()
		{
			#region next modify all the postal codes in tblusers
			// testing data had no hypen in the postal codes.  hyphens were found to be required.
			_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Starting tblUser data modification...", LogEnum.Message);
			sqlComm = sqlConn.CreateCommand();
			sqlComm.CommandType = CommandType.Text;
			sqlComm.CommandText = "SELECT * from tblUsers";
			SqlDataAdapter adap = new SqlDataAdapter(sqlComm);
			DataSet ds = new DataSet();
			sqlConn.Open();
			adap.Fill(ds);
			sqlConn.Close();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				string oldCode = dr["fldUser_PostalCode"].ToString().Trim();
				string newCode = "";
				int x = 0;
				foreach (char c in oldCode)
				{
					newCode += c.ToString();
					if (x == 2)
						newCode += "-";
					x++;
				}
				// now put the new code back
				sqlComm.CommandText = "UPDATE tblUsers set fldUser_PostalCode='" + newCode + "' where fldUser_id=" + dr["fldUser_id"].ToString();
				sqlConn.Open();
				sqlComm.ExecuteNonQuery();
				sqlConn.Close();
			}
			_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "tblUser data modified!", LogEnum.Message);
			#endregion

		}

		public void ReAddFKConstraintOnTblUsers()
		{
			#region last, re-add the FK contraint on tblUsers
			_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Re-adding Constraint...", LogEnum.Message);
			sqlComm = sqlConn.CreateCommand();
			sqlComm.CommandType = CommandType.Text;
			var sqlString = "ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblPostalCodes] FOREIGN KEY([fldUser_PostalCode])";
			sqlString += " REFERENCES [dbo].[tblPostalCodes] ([fldPostal_Code])";
			sqlComm.CommandText = sqlString;
			sqlConn.Open();
			sqlComm.ExecuteNonQuery();
			sqlString = "ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblPostalCodes]";
			sqlComm.CommandText = sqlString;
			sqlComm.ExecuteNonQuery();
			sqlConn.Close();
			_log.WriteToLog(_projectInfo.ProjectLogType, "Main", "Constraint Re-added!", LogEnum.Message);
			Console.WriteLine("Finished...Press Enter to close.");
			Console.ReadLine();
			#endregion

		}

	}
}
