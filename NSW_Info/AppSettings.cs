using System;
using System.Configuration;
using System.Runtime.CompilerServices;


namespace NSW.Info
{
    /// <summary>
    /// contains methods for reading/writing values to config files
    /// </summary>
    public class AppSettings
    {
		//private static readonly IConfiguration _configuration;
        public AppSettings()
        {
        }

		//public AppSettings(IConfiguration configuration)
		//{
		//	_configuration = configuration;
		//}

		/// <summary>
		/// decrypts the requested appsetting in memory
		/// </summary>
		/// <param name="settingName">setting name to decrypt</param>
		/// <returns>string of unencrypted data</returns>
		private static string DecryptAppSetting(string settingName)
        {
			//Byte[] b = Convert.FromBase64String(ConfigurationManager.AppSettings[settingName]);
			//string decryptedConnectionString = System.Text.ASCIIEncoding.ASCII.GetString(b);
			//return decryptedConnectionString;
			return string.Empty;
        }

        /// <summary>
        /// gets appsetting with requested key
        /// </summary>
        /// <param name="settingName">requested key</param>
        /// <param name="encrypted">if true, decrypts the setting before returning the value</param>
        /// <returns>string value of requested setting</returns>
        public static string GetAppSetting(string settingName, bool encrypted = false)
        {
            string returnValue = "";
            if (encrypted)
            {
                returnValue = DecryptAppSetting(settingName);
            }
            else
            {
				//try
				//{
				//	returnValue = ConfigurationManager.AppSettings[settingName].ToString();
				//}
				//catch
				//{
				//	try
				//	{
				//		returnValue = _configuration.GetSection(settingName).Value.ToString();
				//	}
				//	catch (Exception)
				//	{
				//		// eat the error
				//	}
				//}
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
            //ConfigurationManager.AppSettings.Set(keyName, keyValue);
        }
    }
}
