using System;
using System.Web.UI.WebControls;

namespace NSW
{
    public partial class Contact : PageBase
    {
        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLabelValues();
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            this.ContactUsTitle.Text = NSW.Data.LabelText.Text("ContactUs.ContactUsTitle");
            this.ContactUsInstructions.Text = NSW.Data.LabelText.Text("ContactUs.ContactUsInstructions");

            (this.ContactUsWizardStep0.FindControl("ContactEmailLabel") as Label).Text = NSW.Data.LabelText.Text("ContactUs.ContactEmailLabel");
            (this.ContactUsWizardStep0.FindControl("ContactBodyLabel") as Label).Text = NSW.Data.LabelText.Text("ContactUs.ContactBodyLabel");
            (this.ContactUsWizardStep0.FindControl("ContactEmailRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUs.ContactEmailRequired");
            (this.ContactUsWizardStep0.FindControl("ContactBodyRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUs.ContactBodyRequired");
            (this.ContactUsWizardStep0.FindControl("IsEmail") as RegularExpressionValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUs.IsEmail");
            (this.ContactUsWizardStep0.FindControl("ContactCaptchaRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUs.ContactCaptchaRequired");
            (Global.GetControlFromWizard(this.ContactUsWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("ContactUs.FinishButton");

        }

        /// <summary>
        /// validates page values and creates email data object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            this.Captcha1.ValidateCaptcha(this.ContactCaptcha.Text.Trim().ToLower());
            if (Captcha1.UserValidated)
            {
                try
                {
                    NSW.Info.EmailMessage email = new Info.EmailMessage();
                    email.Body = this.ContactBody.Text.Trim();
                    email.From = new System.Net.Mail.MailAddress(this.ContactEmail.Text.Trim());
                    email.Subject = NSW.Data.LabelText.Text("ContactUs.Subject");
                    email.To.Add(NSW.Info.AppSettings.GetAppSetting("AdminEmails", false));
                    email.Send();
                    Response.Redirect("ContactSuccess.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception x)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Contact.FinishClick", x, LogEnum.Critical);
                    Response.Redirect("ContactFailure.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
                this.ContactUsErrorMessage.Text = NSW.Data.LabelText.Text("");
        }
    }
}