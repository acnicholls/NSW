using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSW.Account
{
    public partial class ChangePassword : PageBase
    {
        /// <summary>
        /// loads the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLabelValues();
        }

        /// <summary>
        /// loads all text on the page
        /// </summary>
        private void LoadLabelValues()
        {
            // user instructions text
            this.PageTitle.Text = NSW.Data.LabelText.Text("ChangePass.Title");
            this.Instructions.Text = NSW.Data.LabelText.Text("ChangePass.Instructions");
            NSW.Data.Security.MembershipProvider mp = new Data.Security.MembershipProvider();
            string message = NSW.Data.LabelText.Text("ChangePass.Instructions2.Part1").Trim() + " " + mp.MinRequiredPasswordLength.ToString() + " " + NSW.Data.LabelText.Text("ChangePass.Instructions2.Part2").Trim();
            // wizard text            
            (this.ChangePasswordWizard.FindControl("CurrentPasswordLabel") as Label).Text = NSW.Data.LabelText.Text("ChangePass.CurrentPasswordLabel");
            (this.ChangePasswordWizard.FindControl("CurrentPasswordRequired") as RequiredFieldValidator).Text = NSW.Data.LabelText.Text("ChangePass.CurrentPasswordRequired");
            (this.ChangePasswordWizard.FindControl("NewPasswordLabel") as Label).Text = NSW.Data.LabelText.Text("ChangePass.NewPasswordLabel");
            (this.ChangePasswordWizard.FindControl("NewPasswordRequired") as RequiredFieldValidator).Text = NSW.Data.LabelText.Text("ChangePass.NewPasswordRequired");
            (this.ChangePasswordWizard.FindControl("ConfirmNewPasswordLabel") as Label).Text = NSW.Data.LabelText.Text("ChangePass.ConfirmNewPasswordLabel");
            (this.ChangePasswordWizard.FindControl("ConfirmNewPasswordRequired") as RequiredFieldValidator).Text = NSW.Data.LabelText.Text("ChangePass.ConfirmNewPasswordRequired");
            (this.ChangePasswordWizard.FindControl("NewPasswordCompare") as CompareValidator).Text = NSW.Data.LabelText.Text("ChangePass.NewPasswordCompare");
            (Global.GetControlFromWizard(this.ChangePasswordWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "ChangePasswordPushButton") as Button).Text = NSW.Data.LabelText.Text("ChangePass.ChangePasswordPushButton"); 

        }

        /// <summary>
        /// checks form values for validity and performs page operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                // TODO: hash password
                string newPass = this.ConfirmNewPassword.Text.Trim();
                NSW.Data.User currentUser = new Data.User(Page.User.Identity.Name.ToString());
                currentUser.changePassword(newPass);
                Response.Redirect("ChangePasswordSuccess.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ChangePassword.FinishClick", x, LogEnum.Critical);
            }
        }
    }
}
