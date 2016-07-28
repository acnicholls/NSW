using System;
using System.Reflection;
using System.Web;

namespace NSW.Info
{
    public class ProjectInfo
    {
        public static string Version
        {
            get
            {
                Assembly a = Assembly.GetExecutingAssembly();
                AssemblyName an = a.GetName();
                return Convert.ToString(an.Version);
            }
        }
        public static LogTypeEnum ProjectLogType
        {
            get
            {
                LogTypeEnum returnValue = new LogTypeEnum();
                switch (NSW.Info.AppSettings.GetAppSetting("LogType", false))
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
        public static string PortalLogProcedure
        {
            get
            {
                return NSW.Info.AppSettings.GetAppSetting("LogSproc", false);
            }
        }
        public static string LogLocation
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.ToString() + NSW.Info.AppSettings.GetAppSetting("LogLocation", false);
            }
        }
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
        public static string basicProtocol
        {
            get
            {
                string returnValue = null;
                returnValue = "http://";
                return returnValue;
            }
        }
        public static string secureProtocol
        {
            get
            {
                string returnValue = null;
                returnValue = "https://";
                return returnValue;
            }
        }
        public static string webServer
        {
            get
            {
                return NSW.Info.AppSettings.GetAppSetting("webServer", false);
            }
        }
        public static string AdminEmailList = "ac.nicholls@gmail.com;natsuko.thai@gmail.com";
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
        public static string CurrencySymbol
        {

            get
            {
                return "¥";
            }
        }
    }
}
