using System;

namespace NSW.Account
{
    public partial class ChangePasswordSuccess : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ChangePass.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ChangePass.Success");

        }
    }
}
