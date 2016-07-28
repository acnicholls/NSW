using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace NSW.Account
{
    public partial class Login : PageBase
    {
        /// <summary>
        /// loads the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // load all text values
            this.LoadLabelValues(); 
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        /// <summary>
        /// pulls form data and validates the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            NSW.Data.Security.MembershipProvider mp = new Data.Security.MembershipProvider();
            TextBox username = (TextBox)this.LoginUserWizardStep0.FindControl("UserName");
            TextBox password = (TextBox)this.LoginUserWizardStep0.FindControl("Password");
            CheckBox remember = (CheckBox)this.LoginUserWizardStep0.FindControl("RememberMe");
            if (mp.ValidateUser(username.Text.Trim(), password.Text.Trim()))
            {
                FormsAuthentication.SetAuthCookie(username.Text.Trim(), remember.Checked);
                string url = "~/Default.aspx";
                if (Request.QueryString["ReturnUrl"] != null)
                    url = Request.QueryString["ReturnUrl"].ToString();
                Response.Redirect(url, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                (this.LoginUserWizardStep0.FindControl("FailureText") as Literal).Text = NSW.Data.LabelText.Text("Login.LoginFailMsg");
            }
        }

        /// <summary>
        /// loads the page text
        /// </summary>
        private void LoadLabelValues()
        {
            // now pass the proper values to the page objects
            this.LoginPageTitle.Text = NSW.Data.LabelText.Text("Login.LoginPageTitle");
            this.LoginPageInstructions.Text = NSW.Data.LabelText.Text("Login.LoginPageInst");
            this.RegisterHyperLink.Text = NSW.Data.LabelText.Text("Login.RegisterHyperLink");
            this.RegisterInstructions.Text = NSW.Data.LabelText.Text("Login.RegisterInstructions");
            this.ForgotPasswordLink.Text = NSW.Data.LabelText.Text("Login.ForgotPassword");
            (this.LoginUserWizardStep0.FindControl("UserNameLabel") as Label).Text = NSW.Data.LabelText.Text("Login.UserNameLabel");
            (this.LoginUserWizardStep0.FindControl("PasswordLabel") as Label).Text = NSW.Data.LabelText.Text("Login.PasswordLabel");
            (this.LoginUserWizardStep0.FindControl("PasswordRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Login.PassRequired");
            (this.LoginUserWizardStep0.FindControl("UserNameRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Login.UserRequired");
            (this.LoginUserWizardStep0.FindControl("RememberMeLabel") as Label).Text = NSW.Data.LabelText.Text("Login.RememberMe");
            (Global.GetControlFromWizard(this.LoginWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "LoginButton") as Button).Text = NSW.Data.LabelText.Text("Login.btnLogin");
        }

        protected void ForgotPasswordLink_Click(object sender, EventArgs e)
        {
            string url = "ForgotPassword.aspx";
            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }

    }
}
