using System;
using System.Data;
using System.Data.SqlClient;

namespace NSW.Data.Security
{
    public class RoleProvider : System.Web.Security.SqlRoleProvider
    {
        SqlConnection roleConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
        SqlCommand roleComm = new SqlCommand();

        /// <summary>
        /// constructor
        /// </summary>
        public RoleProvider()
        {
            try
            {

            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "RoleProvider", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// override to determine if user is member of input role
        /// </summary>
        /// <param name="username">user's email</param>
        /// <param name="roleName">name of role to check</param>
        /// <returns>true if true, false if false</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            bool returnValue = false;
            try
            {
                // check the database
                roleComm = roleConn.CreateCommand();
                roleComm.CommandType = CommandType.Text;
                roleComm.CommandText = "Select fldUser_Role from tblUsers where fldUser_Email='" + username + "'";
                roleConn.Open();
                object dbValue = roleComm.ExecuteScalar();
                roleConn.Close();
                string roleString = dbValue.ToString();
                switch (roleString)
                {
                    case "MEMBER":
                        {
                            if (roleName == "MEMBER")
                                returnValue = true;
                            break;
                        }
                    case "ADMIN":
                        {
                            if (roleName == "MEMBER")
                                returnValue = true;
                            if (roleName == "ADMIN")
                                returnValue = true;
                            break;
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "RoleProvider.IsUserInRole", x, LogEnum.Critical);
            }
            return returnValue;
        }

        /// <summary>
        /// pulls back string array of roles for single user
        /// </summary>
        /// <param name="username">email of user</param>
        /// <returns>string array of roles for selected user</returns>
        public override string[] GetRolesForUser(string username)
        {
            string[] returnValue = null;
            try
            {
                // check the database
                roleComm = roleConn.CreateCommand();
                roleComm.CommandType = CommandType.Text;
                roleComm.CommandText = "Select fldUser_Role from tblUsers where fldUser_Email='" + username + "'";
                SqlDataAdapter adap = new SqlDataAdapter(roleComm);
                DataSet ds = new DataSet();
                roleConn.Open();
                adap.Fill(ds);
                roleConn.Close();
                returnValue = new string[ds.Tables[0].Rows.Count];
                for (int y = 0; y == ds.Tables[0].Rows.Count - 1; y++)
                {
                    returnValue[y] = ds.Tables[0].Rows[y]["fldUser_Role"].ToString();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "RoleProvider.GetRolesForUser", x, LogEnum.Critical);
            }
            return returnValue;

        }
    }
}
