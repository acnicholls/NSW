using NSW.Info.Interfaces;
using NSW.Services.Interfaces;
using System.Net.Mail;

namespace NSW.Services
{
	// this class has been rewritten to use any email servers.  

	/// <summary>
	/// Provides a message object that sends the email through gmail. 
	/// EmailMessage is inherited from <c>System.Net.Mail.MailMessage</c>, so all the mail message features are available.
	/// </summary>
	public class EmailService : IEmailService
    {

		#region Private Variables


		private string _Server = "";
		private long _Port = 0;
		private string _From = "";
		private string _UserName = "";
		private string _Password = "";
		private long _MaxAttachmentLength = 0;
		private bool _UseSSL = false;
		private bool _ReqSecurity = false;
		private readonly IProjectInfo _projectInfo;
		private readonly ILog _log;
        #endregion

        #region Public Members

        /// <summary>
        /// Constructor, creates the GmailMessage object
        /// </summary>
        public EmailService(
			IProjectInfo projectInfo,
			IAppSettings settings,
			ILog log
			)
        {
            try 
			{
				_projectInfo = projectInfo;
				log.SendEmail += this.Send;
				_log = log;
				_Server = settings.GetAppSetting("MailServer", false);
				_Port = Convert.ToInt64(settings.GetAppSetting("MailPort", false));
				_From = settings.GetAppSetting("MailFrom", false);
				_UserName = settings.GetAppSetting("MailUser", false);
				_Password = settings.GetAppSetting("MailPassword", false);
				_MaxAttachmentLength = Convert.ToInt64(settings.GetAppSetting("MailMaxAttachmentLength", false));
				_UseSSL = Convert.ToBoolean(settings.GetAppSetting("MailRequireSSL", false));
				_ReqSecurity = Convert.ToBoolean(settings.GetAppSetting("MailRequireSecurity", false));
			}
            catch (Exception x)
            {
                log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// Sends the message. If no from address is given the message will be from _UserName
        /// </summary>
        public void Send(MailMessage message)
        {
            try
            {
                if (_Server != "")
                {
                    if (message.From == null)
                    {
						message.From = new MailAddress(_From);
                    }
                    //this.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient newClient = new SmtpClient(_Server, (int)_Port);
                    newClient.Host = _Server;
                    if (_ReqSecurity)
                    {
                        System.Net.NetworkCredential newCreds = new System.Net.NetworkCredential(_UserName, _Password);
                        newClient.UseDefaultCredentials = false;
                        newClient.Credentials = newCreds;
                    }
                    newClient.EnableSsl = _UseSSL;
                    newClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    newClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.Send", ex, LogEnum.Critical);
            }
        }




        /// <summary>
        /// Sends an email through the specified account
        /// </summary>
        /// <param name="toAddress">Recipients email address</param>
        /// <param name="subject">Message subject</param>
        /// <param name="messageBody">Message body</param>
        public void SendFromExternalServer(string toAddress, string subject, string messageBody)
        {
            if (_Server != "")
            {
                try
                {
                    _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer", "Attempting Email to : " + toAddress, LogEnum.Debug);
					MailMessage gMessage = new MailMessage();
                    gMessage.To.Add(new MailAddress(toAddress));
                    gMessage.Subject = subject;
                    gMessage.Body = messageBody;
                    gMessage.From = new MailAddress(_UserName);
                    System.Net.Mail.SmtpClient newClient = new SmtpClient(_Server, (int)_Port);
                    newClient.EnableSsl = false;
                    newClient.Send(gMessage);
                    _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer", "Email Sent to : " + toAddress, LogEnum.Debug);
                }
                catch (Exception ex)
                {
                    _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer", ex, LogEnum.Critical);
                }
            }
        }

        /// <summary>
        /// Sends an email through the specified account
        /// </summary>
        /// <param name="toAddress">Recipients email address</param>
        /// <param name="subject">Message subject</param>
        /// <param name="messageBody">Message body</param>
        /// <param name="messageAttachment">Message attachment - max 5 MB</param>
        public void SendFromExternalServer(string toAddress, string subject, string messageBody, Attachment messageAttachment)
        {
            if (_Server != "")
            {
                try
                {
                    _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer /w attachment", "Attempting Email to : " + toAddress, LogEnum.Debug);
                    MailMessage gMessage = new MailMessage();
                    gMessage.To.Add(new MailAddress(toAddress));
                    gMessage.Subject = subject;
                    gMessage.Body = messageBody;
                    gMessage.From = new MailAddress(_UserName);
                    if (messageAttachment.ContentStream.Length <= _MaxAttachmentLength)
                        gMessage.Attachments.Add(messageAttachment);
                    System.Net.Mail.SmtpClient newClient = new SmtpClient(_Server, (int)_Port);
                    newClient.EnableSsl = false;
                    newClient.Send(gMessage);
                    _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer /w attachment", "Email Sent to : " + toAddress, LogEnum.Debug);
                }
                catch (Exception ex)
                {
                    _log.WriteToLog(_projectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer /w attachment", ex, LogEnum.Critical);
                }
            }
        }
        #endregion

    } 

} 
