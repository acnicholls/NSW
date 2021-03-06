﻿using System;
using System.Web.UI.WebControls;

namespace NSW.Posts
{
    public partial class ContactPostUser : PageBase
    {
        string[] keyPairs;

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            keyPairs = Global.GrabKeyPairs(Request.QueryString.ToString());
            LoadLabelValues();
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            this.ContactUserTitle.Text = NSW.Data.LabelText.Text("ContactUser.ContactUserTitle");
            this.ContactUserInstructions.Text = NSW.Data.LabelText.Text("ContactUser.ContactUserInstructions");

            (this.ContactUserWizardStep0.FindControl("ContactUserEmailLabel") as Label).Text = NSW.Data.LabelText.Text("ContactUser.ContactUserEmailLabel");
            (this.ContactUserWizardStep0.FindControl("ContactBodyLabel") as Label).Text = NSW.Data.LabelText.Text("ContactUser.ContactBodyLabel");
            (this.ContactUserWizardStep0.FindControl("ContactEmailRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUser.ContactEmailRequired");
            (this.ContactUserWizardStep0.FindControl("ContactBodyRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUser.ContactBodyRequired");
            (this.ContactUserWizardStep0.FindControl("IsEmail") as RegularExpressionValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUser.IsEmail");
            (this.ContactUserWizardStep0.FindControl("ContactCaptchaRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("ContactUser.ContactCaptchaRequired");
            (Global.GetControlFromWizard(this.ContactUserWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("ContactUser.FinishButton");
        
        }                  

        /// <summary>
        /// validates form data and calls data operation method
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
                    NSW.Data.Post thisPost = new Data.Post(Convert.ToInt32(Global.KeyPairValue(keyPairs, "postID")));
                    NSW.Info.EmailMessage email = new Info.EmailMessage();
                    string userEmail = this.ContactEmail.Text.Trim();
                    string bodyString = thisPost.Title + "\r\n\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString1") + " " + userEmail + " " + NSW.Data.LabelText.Text("Email.BodyString2") + "\r\n\r\n";
                    bodyString += this.ContactBody.Text.Trim() + "\r\n\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString3") + " " + NSW.Info.ProjectInfo.protocol + NSW.Info.AppSettings.GetAppSetting("webServer", false) + "/Posts/ViewPost.aspx?postID=" + thisPost.ID.ToString() + "\r\n\r\n\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString4") + "\r\n\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString5") + "\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString6") + "\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString7") + "\r\n";
                    bodyString += NSW.Data.LabelText.Text("Email.BodyString8") + "\r\n";

                    email.Body = bodyString;
                    email.From = new System.Net.Mail.MailAddress(userEmail);
                    email.Subject = NSW.Data.LabelText.Text("Email.Subject");
                    NSW.Data.User postUser = thisPost.PostUser();
                    email.To.Add(postUser.Email);
                    email.Send();
                    Response.Redirect("ContactPostUserSuccess.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception x)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ContactPostUser.FinishClick", x, LogEnum.Critical);
                    Response.Redirect("ContactPostUserFailure.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                this.ContactUserErrorMessage.Text = NSW.Data.LabelText.Text("ContactUser.BadCaptcha");
            }
        }
    }
}