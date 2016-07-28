﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace NSW.Data
{
    public class LabelText
    {
        public string ID { get; set; }
        public string English { get; set; }
        public string Japanese { get; set; }

        private SqlConnection labelConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);

        public LabelText()
        {
               
        }

        public LabelText(string identifier)
        {
            SqlConnection textConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                SqlCommand labelComm = textConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(labelComm);
                labelComm.CommandType = CommandType.Text;
                labelComm.CommandText = "Select * from tblLabelText where fldLabel_ID='" + identifier + "'";
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText", labelComm.CommandText, LogEnum.Debug);
                // first find the user row in the database
                textConn.Open();
                adap.Fill(ds);
                textConn.Close();
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
                this.ID = dr["fldLabel_ID"].ToString();
                English = dr["fldLabel_English"].ToString();
                Japanese = dr["fldLabel_Japanese"].ToString();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText", x, LogEnum.Critical);
            }
        }

        public static string Text(string ID)
        {
            SqlConnection textConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                SqlCommand labelComm = textConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(labelComm);
                labelComm.CommandType = CommandType.Text;
                labelComm.CommandText = "Select * from tblLabelText where fldLabel_ID='" + ID + "'";
                // first find the user row in the database
                textConn.Open();
                adap.Fill(ds);
                textConn.Close();
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
                switch (DisplayLanguage)
                {
                    case "English":
                        {
                            return dr["fldLabel_English"].ToString();
                        }
                    case "Japanese":
                        {
                            return dr["fldLabel_Japanese"].ToString();
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.Text", x, LogEnum.Critical);
            }
            return string.Empty;
        }

        public static string DisplayLanguage
        {
            get
            {
                try
                {
                    string language = "";
                    // check the session variable
                    try
                    {
                        object sessLang = HttpContext.Current.Session;
                        if (sessLang != null)
                            language = HttpContext.Current.Session["DisplayLanguage"].ToString();
                    }
                    catch (NullReferenceException)
                    { }
                    // check the cookie
                    HttpCookie langCook = HttpContext.Current.Request.Cookies["LanguageCookie"];
                    if (langCook != null)
                        language = langCook.Value.ToString();
                    // check user preference
                    if (language == "")
                    {
                        Data.User curUser = new User(HttpContext.Current.User.Identity.Name.ToString());
                        if (Data.User.Exists(curUser.Email))
                            language = ((LanguagePreference)curUser.LanguagePreference).ToString();
                    }
                    // if not set, set it to default
                    if (language == "")
                        language = NSW.Info.ProjectInfo.DefaultLanguage;
                    return language;
                }
                catch (Exception x)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.DisplayLanguage", x, LogEnum.Critical);
                }
                return null;
            }
        }


        public static string Text(string ID, Data.User curUser)
        {
            SqlConnection textConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                SqlCommand labelComm = textConn.CreateCommand();
                SqlDataAdapter adap = new SqlDataAdapter(labelComm);
                labelComm.CommandType = CommandType.Text;
                labelComm.CommandText = "Select * from tblLabelText where fldLabel_ID='" + ID + "'";
                // first find the user row in the database
                textConn.Open();
                adap.Fill(ds);
                textConn.Close();
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
                string language = ((LanguagePreference)curUser.LanguagePreference).ToString();
                switch (language)
                {
                    case "English":
                        {
                            return dr["fldLabel_English"].ToString();
                        }
                    case "Japanese":
                        {
                            return dr["fldLabel_Japanese"].ToString();
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.Text", x, LogEnum.Critical);
            }
            return string.Empty;
        }

        public void modifyLabel()
        {
            try
            {
                SqlCommand labelComm = labelConn.CreateCommand();
                labelComm.CommandType = CommandType.StoredProcedure;
                labelComm.CommandText = "modifyLabelText";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@id", this.ID);
                labelComm.Parameters.Add(param);
                param = new SqlParameter("@english", this.English);
                labelComm.Parameters.Add(param);
                param = new SqlParameter("@japanese", this.Japanese);
                labelComm.Parameters.Add(param);
                // execute the command
                labelConn.Open();
                labelComm.ExecuteNonQuery();
                labelConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.modifyLabel", x, LogEnum.Critical);
            }
        }

        public void deleteLabel(string ID)
        {
            try
            {
                SqlCommand labelComm = labelConn.CreateCommand();
                labelComm.CommandType = CommandType.StoredProcedure;
                labelComm.CommandText = "deleteLabelText";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@id", this.ID);
                labelComm.Parameters.Add(param);
                // execute the command
                labelConn.Open();
                labelComm.ExecuteNonQuery();
                labelConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.deleteLabel", x, LogEnum.Critical);
            }
        }
        public void insertLabel()
        {
            try
            {
                SqlCommand labelComm = labelConn.CreateCommand();
                labelComm.CommandType = CommandType.StoredProcedure;
                labelComm.CommandText = "insertLabelText";
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@id", this.ID);
                labelComm.Parameters.Add(param);
                param = new SqlParameter("@english", this.English);
                labelComm.Parameters.Add(param);
                param = new SqlParameter("@japanese", this.Japanese);
                labelComm.Parameters.Add(param);
                // execute the command
                labelConn.Open();
                labelComm.ExecuteNonQuery();
                labelConn.Close();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.insertLabel", x, LogEnum.Critical);
            }
        }
    }
}
