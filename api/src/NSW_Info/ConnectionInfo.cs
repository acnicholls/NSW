using NSW.Info.Interfaces;
using Microsoft.Extensions.Configuration;

namespace NSW.Info
{
	public interface IConnectionInfo
	{
		string ConnectionString { get; }
	}
    /// <summary>
    /// Connection Info holds information regarding the data connection
    /// </summary>
    public class ConnectionInfo : IConnectionInfo
    {
		private readonly IAppSettings _appSettings;
		private readonly IConfiguration _configuration;
		public ConnectionInfo(
			IAppSettings appSettings,
			IConfiguration configuration
			)
		{
			_appSettings = appSettings;
			_configuration = configuration;
		}
        /// <summary>
        /// returns the string value of 'ConnectionString'
        /// </summary>
        public string ConnectionString
        {
            get
            {
				var connString = _configuration.GetConnectionString(_appSettings.GetAppSetting("ConnectionString"));
				return connString;
            }
        }
    }
}
