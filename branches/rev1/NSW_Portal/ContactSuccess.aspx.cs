using System;

namespace NSW
{
    public partial class ContactSuccess : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ContactUsSuccess.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ContactUsSuccess.Message");

        }
    }
}
