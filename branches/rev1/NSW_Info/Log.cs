﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace NSW
{	//////	Debug = 0,
    //////	Message = 1,
    //////	Important = 2,
    //////	Access = 3,
    //////	Warning = 4,
    //////	Error = 5,
    //////	Critical = 6

    /// <summary>
    /// contains methods for writing to log
    ///</summary>
    public class Log
    {
        private static SqlConnection conACN;
        private static SqlCommand comACN;

        public Log()
        { }
        public static void WriteToLog(LogTypeEnum type, string caller, string message, LogEnum import)
        {
#if !DEBUG

            if (import != LogEnum.Debug)
            {
#endif
            switch (type)
            {
                case LogTypeEnum.File:
                    {
                        WriteToFile(caller, message, import);
                        break;
                    }
                case LogTypeEnum.Database:
                    {
                        WriteToSQL(caller, message, import);
                        break;
                    }
            }
#if !DEBUG
            if (import == LogEnum.Critical)
            {
                Info.EmailMessage msg = new Info.EmailMessage();
                msg.Subject = "Mikechi.com experienced an error...";
                string msgBody = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n";
                msgBody += "ERROR :" + "\r\n\r\n" + message + "\r\n\r\n";
                msg.Body = msgBody;
                msg.From = new System.Net.Mail.MailAddress(NSW.Info.AppSettings.GetAppSetting("MailFrom", false));
                msg.To.Add("ac.nicholls@gmail.com");
                msg.Send();
            }

            }
#endif
#if DEBUG
            if (import != LogEnum.Debug)
                WriteToLog(type, caller, message, LogEnum.Debug);
#endif
        }

        public static void WriteToLog(LogTypeEnum type, string caller, Exception ex, LogEnum import)
        {
#if !DEBUG
			if(import != LogEnum.Debug)
          {
#endif
            switch (type)
            {
                case LogTypeEnum.File:
                    {
                        WriteToFile(caller, ex, import);
#if DEBUG
                        if (import != LogEnum.Debug)
                            WriteToFile(caller, ex, LogEnum.Debug);
#endif
                        break;
                    }
                case LogTypeEnum.Database:
                    {
                        WriteToSQL(caller, ex, import);
                        break;
                    }
            }
#if !DEBUG
            if (import == LogEnum.Critical)
            {
                Info.EmailMessage msg = new Info.EmailMessage();
                msg.Subject = "Mikechi.com experienced an error...";
                string msgBody = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n";
                msgBody += "ERROR :" + "\r\n\r\n" + ex.Message + "\r\n\r\n";
                if (ex.InnerException != null)
                {
                    msgBody += ex.InnerException.Message + "\r\n\r\n";
                }
                msgBody += ex.StackTrace.ToString();
                msg.Body = msgBody;
                msg.From = new System.Net.Mail.MailAddress(NSW.Info.AppSettings.GetAppSetting("MailFrom", false));
                msg.To.Add("ac.nicholls@gmail.com");
                msg.Send();
            }

            }
#endif
        }

        private static void WriteToSQL(string caller, Exception ex, LogEnum import)
        {
            try
            {
                string strMessage = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n";
                strMessage = ex.ToString() + "\r\n";
                strMessage += ex.Message.ToString() + "\r\n";
                strMessage += ex.StackTrace.ToString(); //.InnerException.Message.ToString();
                WriteToDatabase(caller, strMessage, Convert.ToInt32(import));
            }
            catch (Exception x)
            {
                WriteToFile("WriteToSQL with exception", x.Message, LogEnum.Critical);
            }
        }

        private static void WriteToDatabase(string caller, string message, int import)
        {
            try
            {
                conACN = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                comACN = conACN.CreateCommand();
                comACN.CommandType = CommandType.StoredProcedure;
                comACN.CommandText = NSW.Info.ProjectInfo.PortalLogProcedure;
                comACN.Parameters.AddWithValue("@caller", caller);
                comACN.Parameters.AddWithValue("@message", message);
                comACN.Parameters.AddWithValue("@import", import);
                if (conACN.State != ConnectionState.Open)
                    conACN.Open();
                int result = comACN.ExecuteNonQuery();
                conACN.Close();
            }
            catch (Exception x)
            {
                WriteToFile("WriteToDatabase", x.Message, LogEnum.Critical);
            }

        }


        private static void WriteToSQL(string caller, string message, LogEnum import)
        {
            try
            {
                string strMessage = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n";
                strMessage += message + "\r\n";
                WriteToDatabase(caller, strMessage, Convert.ToInt32(import));
            }
            catch (Exception x)
            {
                WriteToFile("WriteToSQL with message", x.Message, LogEnum.Critical);
            }
        }

        private static void WriteToFile(string caller, string message, LogEnum import)
        {
            string fileName = NSW.Info.ProjectInfo.LogLocation + @"\" + import.ToString() + ".txt";
            CheckFolderExists(fileName);
            StreamWriter logFile = new StreamWriter(fileName, true);
            string strMessage = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n";
            strMessage += message + "\r\n";
            logFile.WriteLine(DateTime.Now + ", " + caller + ", " + strMessage);
            logFile.Flush();
            logFile.Close();
        }

        private static void WriteToFile(string caller, Exception ex, LogEnum import)
        {
            string fileName = NSW.Info.ProjectInfo.LogLocation + @"\" + import.ToString() + ".txt";
            CheckFolderExists(fileName);

            StreamWriter logFile = new StreamWriter(fileName, true);
            string strMessage = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n"; 
            strMessage += ex.ToString() + "\r\n";
            strMessage += ex.Message.ToString() + "\r\n";
            logFile.WriteLine(DateTime.Now + ", " + caller + ", " + strMessage);
            logFile.Flush();
            logFile.Close();
        }

        private static bool CheckFolderExists(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            DirectoryInfo folder = file.Directory;
            try
            {
                if (folder.Exists)
                    return true;
                else
                {
                    folder.Create();
                    return true;
                }
            }
            catch (Exception x)
            {
                Log.WriteToFile("Check for Folder", x.Message, LogEnum.Error);
            }
            return false;

        }

    }
}
