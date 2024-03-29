﻿using System;
using System.Net.Mail;


namespace NSW.Info
{
    // this class has been rewritten to use any email servers.  

    /// <summary>
    /// Provides a message object that sends the email through gmail. 
    /// EmailMessage is inherited from <c>System.Net.Mail.MailMessage</c>, so all the mail message features are available.
    /// </summary>
    public class EmailMessage : System.Net.Mail.MailMessage
    {

        #region Private Variables


        private static string _Server = NSW.Info.AppSettings.GetAppSetting("MailServer", false);
        private static long _Port = Convert.ToInt64(NSW.Info.AppSettings.GetAppSetting("MailPort", false));
        private static string _From = NSW.Info.AppSettings.GetAppSetting("MailFrom", false);
        private static string _UserName = NSW.Info.AppSettings.GetAppSetting("MailUser", false);
        private static string _Password = NSW.Info.AppSettings.GetAppSetting("MailPassword", false);
        private static long _MaxAttachmentLength = Convert.ToInt64(NSW.Info.AppSettings.GetAppSetting("MailMaxAttachmentLength", false));
        private static bool _UseSSL = Convert.ToBoolean(NSW.Info.AppSettings.GetAppSetting("MailRequireSSL", false));
        private static bool _ReqSecurity = Convert.ToBoolean(NSW.Info.AppSettings.GetAppSetting("MailRequireSecurity", false));
        #endregion

        #region Public Members

        /// <summary>
        /// Constructor, creates the GmailMessage object
        /// </summary>
        public EmailMessage()
        {
            try { }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// Sends the message. If no from address is given the message will be from _UserName
        /// </summary>
        public void Send()
        {
            try
            {
                if (_Server != "")
                {
                    if (this.From == null)
                    {
                        this.From = new MailAddress(_From);
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
                    newClient.Send(this);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.Send", ex, LogEnum.Critical);
            }
        }

        #endregion

        #region Static Members


        /// <summary>
        /// Sends an email through the specified account
        /// </summary>
        /// <param name="toAddress">Recipients email address</param>
        /// <param name="subject">Message subject</param>
        /// <param name="messageBody">Message body</param>
        public static void SendFromExternalServer(string toAddress, string subject, string messageBody)
        {
            if (_Server != "")
            {
                try
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer", "Attempting Email to : " + toAddress, LogEnum.Debug);
                    EmailMessage gMessage = new EmailMessage();
                    gMessage.To.Add(new MailAddress(toAddress));
                    gMessage.Subject = subject;
                    gMessage.Body = messageBody;
                    gMessage.From = new MailAddress(_UserName);
                    System.Net.Mail.SmtpClient newClient = new SmtpClient(_Server, (int)_Port);
                    newClient.EnableSsl = false;
                    newClient.Send(gMessage);
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer", "Email Sent to : " + toAddress, LogEnum.Debug);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer", ex, LogEnum.Critical);
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
        public static void SendFromExternalServer(string toAddress, string subject, string messageBody, Attachment messageAttachment)
        {
            if (_Server != "")
            {
                try
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer /w attachment", "Attempting Email to : " + toAddress, LogEnum.Debug);
                    EmailMessage gMessage = new EmailMessage();
                    gMessage.To.Add(new MailAddress(toAddress));
                    gMessage.Subject = subject;
                    gMessage.Body = messageBody;
                    gMessage.From = new MailAddress(_UserName);
                    if (messageAttachment.ContentStream.Length <= _MaxAttachmentLength)
                        gMessage.Attachments.Add(messageAttachment);
                    System.Net.Mail.SmtpClient newClient = new SmtpClient(_Server, (int)_Port);
                    newClient.EnableSsl = false;
                    newClient.Send(gMessage);
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer /w attachment", "Email Sent to : " + toAddress, LogEnum.Debug);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EmailMessage.SendFromExternalServer /w attachment", ex, LogEnum.Critical);
                }
            }
        }
        #endregion

    } 

} 
