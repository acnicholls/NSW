using static NSW.Log;

namespace NSW.Info.Interfaces
{
	public interface ILog
	{
		event SendMail SendEmail;
		void WriteToLog(LogTypeEnum type, string caller, string message, LogEnum import);
		void WriteToLog(LogTypeEnum type, string caller, Exception ex, LogEnum import);
	}
}
