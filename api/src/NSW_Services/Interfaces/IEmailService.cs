using System.Net.Mail;

namespace NSW.Services.Interfaces
{
	public interface IEmailService
	{
		void Send(MailMessage message);
		void SendFromExternalServer(string toAddress, string subject, string messageBody);
		void SendFromExternalServer(string toAddress, string subject, string messageBody, Attachment messageAttachment);
	}
}
