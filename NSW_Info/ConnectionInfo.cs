using System.Configuration;

namespace NSW.Info
{
    /// <summary>
    /// Connection Info holds information regarding the data connection
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// returns the string value of 'ConnectionString'
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[AppSettings.GetAppSetting("ConnectionString", false)].ConnectionString;
            }
        }
    }
}
