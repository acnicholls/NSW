using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace NSW.Posts
{
    public partial class RenewPost : PageBase
    {
        string[] keyPairs;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                keyPairs = Global.GrabKeyPairs(Request.QueryString.ToString());
                int postID = Convert.ToInt32(Global.KeyPairValue(keyPairs, "postID"));
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "RenewPost.Page_Load", "Starting...", LogEnum.Debug);
                SqlConnection globConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand globComm = globConn.CreateCommand();
                globComm.CommandType = CommandType.StoredProcedure;
                globComm.CommandText = "RenewPost";
                SqlParameter param = new SqlParameter("@id", postID);
                globComm.Parameters.Add(param);
                globConn.Open();
                globComm.ExecuteNonQuery();
                globConn.Close();
                this.DisplayText.Text = NSW.Data.LabelText.Text("RenewPost.Success");
            }
            catch(Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "RenewPost.Page_Load", x, LogEnum.Critical);
                this.DisplayText.Text = NSW.Data.LabelText.Text("RenewPost.Failure");
            }
        }
    }
}