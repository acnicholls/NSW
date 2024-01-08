namespace NSW.Info.Interfaces
{
	public interface IAppSettings
	{
		string DecryptAppSetting(string settingName);
		string GetAppSetting(string settingName, bool encrypted = false);
		void SetAppSetting(string keyName, string keyValue);
	}
}
