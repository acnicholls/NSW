using System;
using System.Configuration;

namespace NSW.Info
{
    /// <summary>
    /// contains methods for reading/writing values to config files
    /// </summary>
    public class AppSettings
    {
        public AppSettings()
        {
        }

        /// <summary>
        /// decrypts the requested appsetting in memory
        /// </summary>
        /// <param name="settingName">setting name to decrypt</param>
        /// <returns>string of unencrypted data</returns>
        private static string DecryptAppSetting(string settingName)
        {
            Byte[] b = Convert.FromBase64String(ConfigurationManager.AppSettings[settingName]);
            string decryptedConnectionString = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decryptedConnectionString;
        }

        /// <summary>
        /// gets appsetting with requested key
        /// </summary>
        /// <param name="settingName">requested key</param>
        /// <param name="encrypted">if true, decrypts the setting before returning the value</param>
        /// <returns>string value of requested setting</returns>
        public static string GetAppSetting(string settingName, bool encrypted)
        {
            string returnValue = "";
            if (encrypted)
            {
                returnValue = DecryptAppSetting(settingName);
            }
            else
            {
                returnValue = ConfigurationManager.AppSettings[settingName].ToString();
            }
            return returnValue;
        }

        /// <summary>
        /// changes the value of the desired setting
        /// </summary>
        /// <param name="keyName">key of appsetting</param>
        /// <param name="keyValue">new appsetting value</param>
        public static void SetAppSetting(string keyName, string keyValue)
        {
            ConfigurationManager.AppSettings.Set(keyName, keyValue);
        }
    }
}
