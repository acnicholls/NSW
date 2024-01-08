using NSW.Info.Interfaces;
using Microsoft.Extensions.Configuration;

namespace NSW.Info
{
	/// <summary>
	/// contains methods for reading/writing values to config files
	/// </summary>
	public class AppSettings : IAppSettings
    {
		private IConfiguration _configuration;

		public AppSettings(IConfiguration configuration)
        {
			_configuration = configuration;
        }

		/// <summary>
		/// decrypts the requested appsetting in memory
		/// </summary>
		/// <param name="settingName">setting name to decrypt</param>
		/// <returns>string of unencrypted data</returns>
		public string DecryptAppSetting(string settingName)
        {
			Byte[] b = Convert.FromBase64String(_configuration.GetSection(settingName).Value);
			string decryptedConnectionString = System.Text.ASCIIEncoding.ASCII.GetString(b);
			return decryptedConnectionString;
        }

        /// <summary>
        /// gets appsetting with requested key
        /// </summary>
        /// <param name="settingName">requested key</param>
        /// <param name="encrypted">if true, decrypts the setting before returning the value</param>
        /// <returns>string value of requested setting</returns>
        public string GetAppSetting(string settingName, bool encrypted = false)
        {
            string returnValue = "";
            if (encrypted)
            {
                returnValue = DecryptAppSetting(settingName);
            }
            else
            {
				var configSection = _configuration.GetSection(settingName);
				if (configSection.Value != null)
				{
					returnValue = configSection.Value;
				}
			}
            return returnValue;
        }

        /// <summary>
        /// changes the value of the desired setting
        /// </summary>
        /// <param name="keyName">key of appsetting</param>
        /// <param name="keyValue">new appsetting value</param>
        public  void SetAppSetting(string keyName, string keyValue)
        {
			_configuration.GetSection(keyName).Value = keyValue;
        }
    }
}
