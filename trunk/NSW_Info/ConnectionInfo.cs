using System;
using System.Runtime.InteropServices;

namespace NSW.Info
{
    /// <summary>
    /// Connection Info holds information regarding the data connection
    /// </summary>
    public class ConnectionInfo
    {

        public ConnectionInfo()
        {
        }
        #region hashCode
        private static string hashCode = "987687534hflkdfOUIHBH*690H()(%^%IBohkdfj543pi7*&(UHuhf43pkhJHGlyig";
        #endregion

        /// <summary>
        /// returns a hascode 'password'
        /// </summary>
        public static string HashCode
        {
            get { return hashCode; }
        }

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

        /// <summary>
        /// return the appsetting value of 'webServer'
        /// </summary>
        public static string WebServer
        {
            get
            {
                return NSW.Info.AppSettings.GetAppSetting("webServer", false);
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// checks to see if current PC is connected to the internet
        /// </summary>
        /// <returns>true if connected, false if not</returns>
        public static bool IsConnected()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
    }
}
