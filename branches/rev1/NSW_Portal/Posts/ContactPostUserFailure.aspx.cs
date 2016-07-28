using System;

namespace NSW.Posts
{
    public partial class ContactPostUserFailure : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ContactFailure.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ContactFailure.Message");

        }
    }
}
