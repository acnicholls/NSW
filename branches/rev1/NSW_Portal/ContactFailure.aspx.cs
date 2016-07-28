using System;

namespace NSW
{
    public partial class ContactFailure : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ContactUsFailure.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ContactUsFailure.Message");

        }
    }
}
