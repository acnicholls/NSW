using System;
using System.Runtime.InteropServices;

namespace NSW.Info
{
    /// <summary>
    /// Connection Info holds information regarding the data connection
    /// </summary>
    public class ConnectionInfo
    {

        /// <summary>
        /// returns the appsetting value of 'ConnectionString'
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return NSW.Info.AppSettings.GetAppSetting("ConnectionString", false);
            }
        }

    }
}
