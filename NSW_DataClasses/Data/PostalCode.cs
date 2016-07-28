using System;
using System.Data;
using System.Data.SqlClient;

namespace NSW.Data
{
    public class PostalCode
    {
        public string Code { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude{ get; set; }

        /// <summary>
        /// builds a postal code object based on string value ID
        /// </summary>
        /// <param name="Code">ID of the postal code desired</param>
        public PostalCode(string Code)
        {
            try
            {
                SqlConnection pcConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand pcComm = pcConn.CreateCommand();
                pcComm.CommandType = CommandType.Text;
                pcComm.CommandText = "Select * from tblPostalCodes where fldPostal_Code='" + Code + "'";
                SqlDataAdapter adap = new SqlDataAdapter(pcComm);
                DataSet ds = new DataSet();
                pcConn.Open();
                adap.Fill(ds);
                pcConn.Close();
                if (ds.Tables[0].Rows.Count == 1)
                {
                    this.Code = ds.Tables[0].Rows[0]["fldPostal_Code"].ToString();
                    this.Longitude = Convert.ToDecimal(ds.Tables[0].Rows[0]["fldPostal_Longitude"]);
                    this.Latitude = Convert.ToDecimal(ds.Tables[0].Rows[0]["fldPostal_Latitude"]);
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostalCode.ByCode", x, LogEnum.Critical);
            }
        }

    }
}
