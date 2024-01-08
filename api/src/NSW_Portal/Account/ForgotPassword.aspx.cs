using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSW.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLabelValues();
        }

        private void LoadLabelValues()
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ForgotPass.Title");
            this.Instructions.Text = NSW.Data.LabelText.Text("ForgotPass.Instructions");
            (this.ForgotPasswordWizard.FindControl("ForgotPasswordLabel") as Label).Text = NSW.Data.LabelText.Text("ForgotPass.ForgotPasswordLabel");
            (Global.GetControlFromWizard(this.ForgotPasswordWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "ForgotPasswordPushButton") as Button).Text = NSW.Data.LabelText.Text("ForgotPass.ForgotPasswordPushButton");
        }

        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                NSW.Data.User fpUser = new Data.User(this.ForgotPasswordUser.Text.Trim());

                if (fpUser.ID != 0)
                {
                    // reset the useer's password
                    string newPassword = NSW.Info.RandomFunctions.BuildNewPassword();
                    fpUser.changePassword(newPassword);
                    fpUser.Password = newPassword;
                    // send the new password to the registered email address
                    NSW.Info.EmailMessage email = new Info.EmailMessage();
                    System.Net.Mail.MailAddress userAddress = new System.Net.Mail.MailAddress(this.ForgotPasswordUser.Text.Trim());
                    email.To.Add(userAddress);
                    email.Subject = NSW.Data.LabelText.Text("ForgotPass.Subject");
                    string strBody = NSW.Data.LabelText.Text("ForgotPass.Line1");
                    strBody += "\r\n\r\n";
                    strBody += NSW.Data.LabelText.Text("ForgotPass.Line2") + " " + fpUser.Password.ToString();
                    strBody += "\r\n\r\n";
                    strBody += NSW.Data.LabelText.Text("ForgotPass.Line3");
                    email.Body = strBody;
                    email.Send();
                    string url = "ForgotPasswordEmailSent.aspx";
                    Response.Redirect(url, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    this.FailureText.Text = "No user with that email found.";
                    e.Cancel = true;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ForgotPassword.FinishClick", x, LogEnum.Critical);
            }
        }
    }
}