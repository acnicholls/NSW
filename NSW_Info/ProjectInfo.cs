using NSW.Info.Interfaces;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace NSW.Info
{
	public class ProjectInfo : IProjectInfo
    {
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IAppSettings _appSettings;
		private readonly ILog _log;
		public ProjectInfo(
			IHttpContextAccessor contextAccessor
, IAppSettings appSettings,
			ILog log

			)
		{
			_contextAccessor = contextAccessor;
			_appSettings = appSettings;
			_log = log;
		}
		/// <summary>
		/// returns the version number of the current assembly
		/// </summary>
		 public  string Version
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
        public  LogTypeEnum ProjectLogType
        {
            get
            {
                LogTypeEnum returnValue = new LogTypeEnum();
                switch (_appSettings.GetAppSetting("LogType", false).ToLower())
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
        public  string PortalLogProcedure
        {
            get
            {
                return _appSettings.GetAppSetting("LogSproc", false);
            }
        }

        /// <summary>
        /// checks the appsetting to determine where file logs are kept
        /// </summary>
        public  string LogLocation
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.ToString() + _appSettings.GetAppSetting("LogLocation", false);
            }
        }

        /// <summary>
        /// checks the current request protocol
        /// </summary>
        public  string protocol
        {
            get
            {
                string returnValue = null;
                try
                {
                    if (_contextAccessor.HttpContext== null)
                        returnValue = basicProtocol;
                    else
                    {
                        if (_contextAccessor.HttpContext.Request.IsHttps)
                            returnValue = secureProtocol;
                        else
                            returnValue = basicProtocol;
                    }
                }
                catch (Exception x)
                {
                    _log.WriteToLog(LogTypeEnum.File, "ProjectInfo.protocol", x, LogEnum.Critical);
                }
                return returnValue;
            }
        }

        /// <summary>
        /// returns a string representing the current protocol
        /// </summary>
        public  string basicProtocol
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
        public  string secureProtocol
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
        public  string webServer
        {
            get
            {
                return _appSettings.GetAppSetting("webServer", false);
            }
        }

        /// <summary>
        /// contains emails for project administration
        /// </summary>
        public  string AdminEmailList => "ac.nicholls@gmail.com;natsuko.thai@gmail.com";

        /// <summary>
        /// return the string value for the default display language of the project
        /// </summary>
        public  string DefaultLanguage
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
        public  string CurrencySymbol
        {

            get
            {
                return "¥";
            }
        }
    }
}
