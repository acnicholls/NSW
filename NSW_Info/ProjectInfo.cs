using System;
using System.Reflection;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace NSW.Info
{
    public class ProjectInfo
    {
        /// <summary>
        /// returns the version number of the current assembly
        /// </summary>
        public static string Version
        {
            get
            {
                Assembly a = Assembly.GetExecutingAssembly();
                AssemblyName an = a.GetName();
                return Convert.ToString(an.Version);
            }
        }

        /// <summary>
        /// checks the appsetting to determine where to log to
        /// </summary>
        public static LogTypeEnum ProjectLogType
        {
            get
            {
                LogTypeEnum returnValue = new LogTypeEnum();
                switch (NSW.Info.AppSettings.GetAppSetting("LogType", false).ToLower())
                {
                    case "database":
                        {
                            returnValue = LogTypeEnum.Database;
                            break;
                        }
                    case "file":
                        {
                            returnValue = LogTypeEnum.File;
                            break;
                        }
                }
                return returnValue;
            }
        }

        /// <summary>
        /// checks the appsetting for the name of the SQL Stored Procedure to send log entries to
        /// </summary>
        public static string PortalLogProcedure
        {
            get
            {
                return NSW.Info.AppSettings.GetAppSetting("LogSproc", false);
            }
        }

        /// <summary>
        /// checks the appsetting to determine where file logs are kept
        /// </summary>
        public static string LogLocation
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.ToString() + NSW.Info.AppSettings.GetAppSetting("LogLocation", false);
            }
        }

        /// <summary>
        /// checks the current request protocol
        /// </summary>
        public static string protocol
        {
            get
            {
                string returnValue = null;
                try
                {
                    if (HttpContext.Current == null)
                        returnValue = basicProtocol;
                    else
                    {
                        if (HttpContext.Current.Request.IsSecureConnection)
                            returnValue = secureProtocol;
                        else
                            returnValue = basicProtocol;
                    }
                }
                catch (Exception x)
                {
                    Log.WriteToLog(LogTypeEnum.File, "ProjectInfo.protocol", x, LogEnum.Critical);
                }
                return returnValue;
            }
        }

        /// <summary>
        /// returns a string representing the current protocol
        /// </summary>
        public static string basicProtocol
        {
            get
            {
                string returnValue = null;
                returnValue = "http://";
                return returnValue;
            }
        }

        /// <summary>
        /// returns a string representing the current protocol
        /// </summary>
        public static string secureProtocol
        {
            get
            {
                string returnValue = null;
                returnValue = "https://";
                return returnValue;
            }
        }

        /// <summary>
        /// returns the appsetting value representing the current web server address
        /// </summary>
        public static string webServer
        {
            get
            {
                return NSW.Info.AppSettings.GetAppSetting("webServer", false);
            }
        }

        /// <summary>
        /// contains emails for project administration
        /// </summary>
        public static string AdminEmailList = "ac.nicholls@gmail.com;natsuko.thai@gmail.com";

        /// <summary>
        /// return the string value for the default display language of the project
        /// </summary>
        public static string DefaultLanguage
        {
            // this can be set depending on a user preference
            // or user location
            // or a selection on the main page of the site.
            get
            {
#if DEBUG
                return "English";
#endif
#if !DEBUG
                return "Japanese";
#endif
            }

        }

        /// <summary>
        /// returns the symbol representing currency for the project
        /// </summary>
        public static string CurrencySymbol
        {

            get
            {
                return "¥";
            }
        }
    }
}
