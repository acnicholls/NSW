using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace NSW_DBUpdater
{
    /// <summary>
    /// this program was written to take the production instance's Japanese text and move them to the testing database
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
            // declare connections
            SqlConnection LocalConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[NSW.Info.AppSettings.GetAppSetting("ConnectionStringLocal", false)].ConnectionString);
            SqlConnection RemoteConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[NSW.Info.AppSettings.GetAppSetting("ConnectionStringRemote", false)].ConnectionString);
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
                    string updateSQL = "UPDATE tblLabelText set fldLabelText_Japanese='" + dr["fldLabelText_Japanese"].ToString() + "'";
                    SqlCommand ldc = new SqlCommand(updateSQL);
                    ldc.Connection = LocalConnection;
                    LocalConnection.Open();
                    int result = ldc.ExecuteNonQuery();
                    LocalConnection.Close();
                }
            }
            catch(Exception x)
            {
                NSW.Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "DBUpdater.Main", x, NSW.LogEnum.Critical);
            }
        }
    }
}
