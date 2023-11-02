namespace NSW.Info.Interfaces
{
	public interface ILog
	{
		void WriteToLog(LogTypeEnum type, string caller, string message, LogEnum import);
		void WriteToLog(LogTypeEnum type, string caller, Exception ex, LogEnum import);
	}
}
