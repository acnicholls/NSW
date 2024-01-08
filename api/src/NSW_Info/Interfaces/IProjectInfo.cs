namespace NSW.Info.Interfaces
{
	public interface IProjectInfo
	{
		string Version { get; }
		LogTypeEnum ProjectLogType { get; }
		string PortalLogProcedure { get; }
		string LogLocation { get; }

		string protocol { get; }

		string basicProtocol { get; }

		string secureProtocol { get; }

		string webServer { get; }
		string AdminEmailList { get; }
		string DefaultLanguage { get; }
		string CurrencySymbol { get; }
	}
}
