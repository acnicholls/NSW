﻿using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSW
{
    public class Global : System.Web.HttpApplication
    {
        BackgroundWorker bw = new BackgroundWorker();

        /// <summary>
        /// performs operations when application starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Start(object sender, EventArgs e)
        {
            //try
            //{
            //    // Code that runs on application startup
            //    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global", "Application_Start", LogEnum.Debug);
            //}
            //catch (Exception x)
            //{
            //    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Application_Start", x, LogEnum.Critical);
            //}
        }

        /// <summary>
        /// performs operations when user begins a page request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                // Code that runs on application startup
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global", "Application_BeginRequest", LogEnum.Debug);
                string msgString = Context.Request.RawUrl.ToString();
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Application_BeginRequest", msgString, LogEnum.Message);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Application_BeginRequest", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// performs operations when application shuts down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_End(object sender, EventArgs e)
        {
            try
            {
                //  Code that runs on application shutdown
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global", "Application_End", LogEnum.Debug);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Application_End", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// performs operations when unhandled errors happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Error(object sender, EventArgs e)
        {
            //// Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError();
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Application_Error", ex, LogEnum.Critical);
        }

        /// <summary>
        /// performs operations during beginning of each session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_Start(object sender, EventArgs e)
        {
            try
            {
                // Code that runs when a new session is started
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global", "Session_Start", LogEnum.Debug);
                string ip = "";
                try
                {
                    ip = Context.Request.UserHostAddress.ToString();
                }
                catch(NullReferenceException)
                {
                    ip = "No IP";
                }
                string msgString = "";
                try
                {
                    msgString = Context.Request.UserAgent.ToString();
                }
                catch(NullReferenceException)
                {
                    msgString = "No UserAgent";
                }
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Session_Start", ip + " " + msgString, LogEnum.Message);
                bw.WorkerReportsProgress = false;
                bw.WorkerSupportsCancellation = false;
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerAsync();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Session_Start", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// performs operations on end of each session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_End(object sender, EventArgs e)
        {
            try
            {
                // Code that runs when a session ends. 
                // Note: The Session_End event is raised only when the sessionstate mode
                // is set to InProc in the Web.config file. If session mode is set to StateServer 
                // or SQLServer, the event is not raised.
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global", "Session_End", LogEnum.Debug);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.Session_End", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// gets the dynamic name of a control on a wizard template
        /// </summary>
        /// <param name="wizard"></param>
        /// <param name="wzdTemplate"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static Control GetControlFromWizard(Wizard wizard, WizardNavigationTempContainer wzdTemplate, string controlName)
        {
            System.Text.StringBuilder strCtrl = new System.Text.StringBuilder();
            strCtrl.Append(wzdTemplate);
            strCtrl.Append("$");
            strCtrl.Append(controlName);

            return wizard.FindControl(strCtrl.ToString());
        }

        private void CheckSessionExpiry()
        {
            if (this.Session.IsNewSession)
            {

            }
        }

        /// <summary>
        /// splits querystring into a string array of key/values
        /// </summary>
        /// <param name="requestQString">page request querystring</param>
        /// <returns>string array of key/values</returns>
        public static string[] GrabKeyPairs(string requestQString)
        {
            string[] keyPairs = null;
            try
            {
                // split the string
                keyPairs = requestQString.Split('&');
                int num = keyPairs.Length;
                Log.WriteToLog(LogTypeEnum.Database, "Global.GrabKeyPairs ", "Number of keypairs : " + num.ToString(), LogEnum.Debug);
            }
            catch (Exception x)
            {
                Log.WriteToLog(LogTypeEnum.Database, "Global.GrabKeyPairs ", x, LogEnum.Critical);
            }
            return keyPairs;
        }

        /// <summary>
        /// checks string array for key name
        /// </summary>
        /// <param name="keyPairs">string array of querystring key/values</param>
        /// <param name="inputString">key name to locate</param>
        /// <returns>true if key exists, false if not</returns>
        public static bool KeyPairContains(string[] keyPairs, string inputString)
        {
            Log.WriteToLog(LogTypeEnum.Database, "Global.KeyPairContains ", "Contains : Looking for : " + inputString, LogEnum.Debug);
            foreach (string keypair in keyPairs)
            {
                string[] values = keypair.Split('=');
                Log.WriteToLog(LogTypeEnum.Database, "Global.KeyPairContains ", "Checking value : " + values[0].ToString(), LogEnum.Debug);
                if (values[0].ToString() == inputString)
                {
                    Log.WriteToLog(LogTypeEnum.Database, "Global.KeyPairContains ", "Found : " + inputString, LogEnum.Debug);
                    return true;
                }
            }
            Log.WriteToLog(LogTypeEnum.Database, "Global.KeyPairContains ", "Not Found.", LogEnum.Debug);
            return false;
        }
      
        /// <summary>
        /// check string array for querystring value
        /// </summary>
        /// <param name="keyPairs">string array of querystring key/values</param>
        /// <param name="inputString">key name to locate</param>
        /// <returns>corresponding value of key/value pair</returns>
        public static string KeyPairValue(string[] keyPairs, string inputString)
        {
            foreach (string keypair in keyPairs)
            {
                string[] values = keypair.Split('=');
                if (values[0].ToString() == inputString)
                {
                    Log.WriteToLog(LogTypeEnum.Database, "Global.KeyPairValue", "Returning Value : " + values[1].ToString(), LogEnum.Debug);
                    return values[1].ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// performs background worker processes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", "Starting...", LogEnum.Message);
                // set all sold posts to be deleted  (give users more time, in case sold was premature)
                SqlConnection globConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand globComm = globConn.CreateCommand();
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", "Expiring posts...", LogEnum.Message);

                // run expire posts procedure
                globComm.CommandType = CommandType.StoredProcedure;
                globComm.CommandText = "ExpirePosts";
                globConn.Open();
                globComm.ExecuteNonQuery();
                globConn.Close();
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", "Sending expiry emails...", LogEnum.Message);

                SendExpiryEmails();
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", "Marking posts for deletion...", LogEnum.Message);

                // mark posts for deletion
                globComm.CommandText = "CheckForDeleteablePosts";
                globConn.Open();
                globComm.ExecuteNonQuery();
                globConn.Close();
                // delete all posts and photos for marked posts.
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", "Deleting photos and posts...", LogEnum.Message);

                globComm.CommandType = CommandType.Text;
                globComm.CommandText = "Select * from tblPosts where fldPost_DeleteFlag = 1";
                SqlDataAdapter adap = new SqlDataAdapter(globComm);
                DataSet ds = new DataSet();
                globConn.Open();
                adap.Fill(ds);
                globConn.Close();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // delete each post and post photos
                    NSW.Data.Post thisPost = new Data.Post(Convert.ToInt32(dr["fldPost_id"]));
                    DirectoryInfo fotoFolder = new DirectoryInfo(Server.MapPath("~/Posts/Photos/" + thisPost.ID.ToString()));
                    if (fotoFolder.Exists)
                    {
                        fotoFolder.Delete(true);
                    }
                    thisPost.deletePost();
               }
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", "Complete.", LogEnum.Message);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.bw_DoWork", ex, LogEnum.Critical);
            }
        }

        /// <summary>
        /// sends email to posts users where post is expired today.
        /// </summary>
        private void SendExpiryEmails()
        {
            try
            {
                DateTime today = DateTime.Today;
                SqlConnection globConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand globComm = globConn.CreateCommand();
                globComm.CommandType = CommandType.Text;
                globComm.CommandText = "Select * from tblPosts where fldPost_Status='EXPIRED' and CAST(fldPost_ChangeDate as date)='" + today.ToString() + "' and fldPost_emailSent=0";
                SqlDataAdapter adap = new SqlDataAdapter(globComm);
                DataSet ds = new DataSet();
                globConn.Open();
                adap.Fill(ds);
                globConn.Close();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // send email to post's user
                    NSW.Data.Post thisPost = new Data.Post(Convert.ToInt32(dr["fldPost_id"]));
                    thisPost.SendExpiryEmail();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Global.SendExpiryEmails", x, LogEnum.Critical);
            }
        }

    }
}
