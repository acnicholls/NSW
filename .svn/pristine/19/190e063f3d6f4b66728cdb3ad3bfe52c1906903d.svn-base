using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace NSW.Admin
{
    public partial class NumbersPage : System.Web.UI.Page
    {
        private SqlConnection defConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
        private SqlCommand defComm;

        protected void Page_Load(object sender, EventArgs e)
        {
            // grab number of posts
            defComm = new SqlCommand("Select count(*) from tblPosts where fldPost_status='ACTIVE'", defConn);
            defComm.CommandType = CommandType.Text;
            defConn.Open();
            object num = defComm.ExecuteScalar();
            defConn.Close();
            Session["NumOfPosts"] = Convert.ToInt32(num);

            // grab number of users
            defComm = new SqlCommand("Select count(*) from tblUsers", defConn);
            defComm.CommandType = CommandType.Text;
            defConn.Open();
            num = defComm.ExecuteScalar();
            defConn.Close();
            Session["NumOfUsers"] = Convert.ToInt32(num);

            // grab number of Labels
            defComm = new SqlCommand("Select count(*) from tblLabelText", defConn);
            defComm.CommandType = CommandType.Text;
            defConn.Open();
            num = defComm.ExecuteScalar();
            defConn.Close();
            Session["NumOfLabels"] = Convert.ToInt32(num);

            // grab number of PostalCodes
            defComm = new SqlCommand("Select count(*) from tblPostalCodes", defConn);
            defComm.CommandType = CommandType.Text;
            defConn.Open();
            num = defComm.ExecuteScalar();
            defConn.Close();
            Session["NumOfCodes"] = Convert.ToInt32(num);

            // grab number of Categories
            defComm = new SqlCommand("Select count(*) from tblPostCategories", defConn);
            defComm.CommandType = CommandType.Text;
            defConn.Open();
            num = defComm.ExecuteScalar();
            defConn.Close();
            Session["NumOfCategories"] = Convert.ToInt32(num);

        }
    }
}