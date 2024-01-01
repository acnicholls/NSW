﻿using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NSW.Info;
using NSW.Info.Interfaces;
using System.Data;
using System.Net.Mail;

namespace NSW
{
	//////	Debug = 0,
	//////	Message = 1,
	//////	Important = 2,
	//////	Access = 3,
	//////	Warning = 4,
	//////	Error = 5,
	//////	Critical = 6

	/// <summary>
	/// contains methods for writing to log
	///</summary>
	public class Log : ILog
    {
        private static SqlConnection conACN;
        private static SqlCommand comACN;

        private static ILogger<Log> _logger;
		private readonly IProjectInfo _projectInfo;
		private readonly IConnectionInfo _connectionInfo;
		private readonly IAppSettings _appSettings;
		private readonly IHttpContextAccessor _contextAccessor;

		public delegate void SendMail(MailMessage msg);

		public event SendMail SendEmail;


        public Log(
			ILogger<Log> logger,
			IProjectInfo projectInfo,
			IConnectionInfo connection,
			IAppSettings appSettings,
			IHttpContextAccessor contextAccessor
			)
        { 
			_logger = logger;
			_projectInfo = projectInfo;
			_connectionInfo = connection;
			_appSettings = appSettings;
			_contextAccessor = contextAccessor;
		}

        /// <summary>
        /// writes a log with string message to the requested data store
        /// </summary>
        /// <param name="type">data store</param>
        /// <param name="caller">calling method</param>
        /// <param name="message">log message</param>
        /// <param name="import">log importance</param>
        public  void WriteToLog(LogTypeEnum type, string caller, string message, LogEnum import)
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

                MailMessage msg = new MailMessage();
                msg.Subject = "Mikechi.com experienced an error...";
                string msgBody = "Requested URL : " + _contextAccessor.HttpContext.Request.Query.ToString() + "\r\n\r\n";
                msgBody += "ERROR :" + "\r\n\r\n" + message + "\r\n\r\n";
                msg.Body = msgBody;
                msg.From = new System.Net.Mail.MailAddress(_appSettings.GetAppSetting("MailFrom", false));
                msg.To.Add("ac.nicholls@gmail.com");
				SendEmail?.Invoke(msg);
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
        public  void WriteToLog(LogTypeEnum type, string caller, Exception ex, LogEnum import)
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
                        WriteToDatabase(caller, ex, import);
                        break;
                    }
            }
#if !DEBUG
            if (import == LogEnum.Critical)
            {
                MailMessage msg = new MailMessage();
                msg.Subject = "Mikechi.com experienced an error...";
                string msgBody = "Requested URL : " + _contextAccessor.HttpContext.Request.Query.ToString() + "\r\n\r\n";
                msgBody += "ERROR :" + "\r\n\r\n" + ex.Message + "\r\n\r\n";
                if (ex.InnerException != null)
                {
                    msgBody += ex.InnerException.Message + "\r\n\r\n";
                }
                msgBody += ex.StackTrace.ToString();
                msg.Body = msgBody;
                msg.From = new System.Net.Mail.MailAddress(_appSettings.GetAppSetting("MailFrom", false));
                msg.To.Add("ac.nicholls@gmail.com");
                SendEmail?.Invoke(msg);
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
        private  void WriteToDatabase(string caller, Exception ex, LogEnum import)
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
        private  void WriteToDatabase(string caller, string message, int import)
        {
            try
            {
                conACN = new SqlConnection(_connectionInfo.ConnectionString);
                comACN = conACN.CreateCommand();
                comACN.CommandType = CommandType.StoredProcedure;
                comACN.CommandText = _projectInfo.PortalLogProcedure;
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
        /// writes the passed message to the passed file, listed as the passed caller
        /// </summary>
        /// <param name="caller">the caller to log as</param>
        /// <param name="message">the message to log</param>
        /// <param name="import">the importance (name) of the log file to log to</param>
        private  void WriteToFile(string caller, string message, LogEnum import)
        {
            try
            {

                string logMessage = caller + System.Environment.NewLine + message;

                switch (import)
                {
                    case LogEnum.Critical:
                        {
                            _logger.LogError(logMessage);
                            break;
                        }
                    case LogEnum.Error:
                        {
                            _logger.LogError(logMessage);
                            break;
                        }
                    case LogEnum.Message:
                        {
                            _logger.LogInformation(logMessage);
                            break;
                        }
                    case LogEnum.Warning:
                        {
                            _logger.LogWarning(logMessage);
                            break;
                        }
                    case LogEnum.Important:
                        {
                            _logger.LogInformation(logMessage);
                            break;
                        }
                    case LogEnum.Debug:
                        {
                            _logger.LogDebug(logMessage);
                            break;
                        }
                    case LogEnum.Access:
                        {
                            _logger.LogInformation(logMessage);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                string fileName = _projectInfo.LogLocation + @"LogFailure.txt";
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
        private  void WriteToFile(string caller, Exception ex, LogEnum import)
        {
            try
            {

                string logMessage = caller + System.Environment.NewLine + ex.Message;

                switch (import)
                {
                    case LogEnum.Critical:
                        {
                            _logger.LogError(ex, logMessage);
                            break;
                        }
                    case LogEnum.Error:
                        {
                            _logger.LogError(ex, logMessage);
                            break;
                        }
                    case LogEnum.Message:
                        {
                            _logger.LogInformation(ex, logMessage);
                            break;
                        }
                    case LogEnum.Warning:
                        {
                            _logger.LogWarning(ex, logMessage);
                            break;
                        }
                    case LogEnum.Important:
                        {
                            _logger.LogInformation(ex, logMessage);
                            break;
                        }
                    case LogEnum.Debug:
                        {
                            _logger.LogDebug(ex, logMessage);
                            break;
                        }
                    case LogEnum.Access:
                        {
                            _logger.LogInformation(ex, logMessage);
                            break;
                        }
                }

            }
            catch (Exception innerException)
            {
                string fileName = _projectInfo.LogLocation + @"LogFailure.txt";
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
        private  bool CheckFolderExists(string fileName)
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
                this.WriteToFile("Check for Folder", x.Message, LogEnum.Error);
            }
            return false;

        }

    }
}
