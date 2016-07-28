using System;

namespace NSW.Account
{
    public partial class ForgotPasswordEmailSent : PageBase
    {
        /// <summary>
        /// loads the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ForgotPasswordEmailSent.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ForgotPasswordEmailSent.Message");

        }
    }
}
