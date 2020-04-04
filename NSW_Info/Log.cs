using NLog;
using System;
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

        private static ILogger logLogger;

        public Log()
        { }

        /// <summary>
        /// writes a log with string message to the requested data store
        /// </summary>
        /// <param name="type">data store</param>
        /// <param name="caller">calling method</param>
        /// <param name="message">log message</param>
        /// <param name="import">log importance</param>
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
                        WriteToDatabase(caller, message, Convert.ToInt32(import));
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

        /// <summary>
        /// writes a log with an System.Exception to the requested data store
        /// </summary>
        /// <param name="type">data store</param>
        /// <param name="caller">calling method</param>
        /// <param name="ex">System.Exception to log details of</param>
        /// <param name="import">log importance</param>
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

        /// <summary>
        /// prepares the message to write to the SQL data store
        /// </summary>
        /// <param name="caller">calling method</param>
        /// <param name="ex">System.Exception to log</param>
        /// <param name="import">log importance</param>
        private static void WriteToSQL(string caller, Exception ex, LogEnum import)
        {
            try
            {
                //string strMessage = "Requested URL : " + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\r\n\r\n";
                //strMessage = ex.ToString() + "\r\n";
                string strMessage = ex.Message.ToString() + "\r\n";
                strMessage += ex.StackTrace.ToString(); //.InnerException.Message.ToString();
                WriteToDatabase(caller, strMessage, Convert.ToInt32(import));
            }
            catch (Exception x)
            {
                WriteToFile("WriteToSQL with exception", x.Message, LogEnum.Critical);
            }
        }

        /// <summary>
        /// calls the stored procedure to write a log to the SQL data store
        /// </summary>
        /// <param name="caller">calling method</param>
        /// <param name="message">message to store</param>
        /// <param name="import">importance of entry</param>
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

        /// <summary>
        /// writes a string log entry to a file
        /// </summary>
        /// <param name="caller">calling method</param>
        /// <param name="message">message to write</param>
        /// <param name="import">name of file/importance of log entry</param>

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
            try
            {
                logLogger = NLog.LogManager.GetLogger("NSW");

                string logMessage = caller + System.Environment.NewLine + message;

                switch (import)
                {
                    case LogEnum.Critical:
                        {
                            logLogger.Error(logMessage);
                            break;
                        }
                    case LogEnum.Error:
                        {
                            logLogger.Error(logMessage);
                            break;
                        }
                    case LogEnum.Message:
                        {
                            logLogger.Info(logMessage);
                            break;
                        }
                    case LogEnum.Warning:
                        {
                            logLogger.Warn(logMessage);
                            break;
                        }
                    case LogEnum.Important:
                        {
                            logLogger.Info(logMessage);
                            break;
                        }
                    case LogEnum.Debug:
                        {
                            logLogger.Debug(logMessage);
                            break;
                        }
                    case LogEnum.Access:
                        {
                            logLogger.Info(logMessage);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                string fileName = NSW.Info.ProjectInfo.LogLocation + @"LogFailure.txt";
                CheckFolderExists(fileName);
                StreamWriter logFile = new StreamWriter(fileName, true);
                logFile.WriteLine(ex.Message);
                logFile.WriteLine(ex.StackTrace);
                logFile.Flush();
                logFile.Close();
            }

        }

        /// <summary>
        /// writes a System.Exception log entry to a file
        /// </summary>
        /// <param name="caller">calling method</param>
        /// <param name="ex">System.Exception to log</param>
        /// <param name="import">name of file/importance of log entry</param>
        private static void WriteToFile(string caller, Exception ex, LogEnum import)
        {
            try
            {
                logLogger = NLog.LogManager.GetLogger("NSW");

                string logMessage = caller + System.Environment.NewLine + ex.Message;

                switch (import)
                {
                    case LogEnum.Critical:
                        {
                            logLogger.Error(ex, logMessage);
                            break;
                        }
                    case LogEnum.Error:
                        {
                            logLogger.Error(ex, logMessage);
                            break;
                        }
                    case LogEnum.Message:
                        {
                            logLogger.Info(ex, logMessage);
                            break;
                        }
                    case LogEnum.Warning:
                        {
                            logLogger.Warn(ex, logMessage);
                            break;
                        }
                    case LogEnum.Important:
                        {
                            logLogger.Info(ex, logMessage);
                            break;
                        }
                    case LogEnum.Debug:
                        {
                            logLogger.Debug(ex, logMessage);
                            break;
                        }
                    case LogEnum.Access:
                        {
                            logLogger.Info(ex, logMessage);
                            break;
                        }
                }

            }
            catch (Exception innerException)
            {
                string fileName = NSW.Info.ProjectInfo.LogLocation + @"LogFailure.txt";
                CheckFolderExists(fileName);
                StreamWriter logFile = new StreamWriter(fileName, true);
                logFile.WriteLine(ex.Message);
                logFile.WriteLine(ex.StackTrace);
                logFile.WriteLine("#######################  Inner Exception  ####################");
                logFile.WriteLine(innerException.Message);
                logFile.WriteLine(innerException.StackTrace);
                logFile.Flush();
                logFile.Close();
            }
        }

        /// <summary>
        /// checks to see if the folder for log entries exists, folder is created if it doesn't exist
        /// </summary>
        /// <param name="fileName">name of folder</param>
        /// <returns>true</returns>
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
