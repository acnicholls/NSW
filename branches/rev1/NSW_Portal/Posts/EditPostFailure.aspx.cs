using System;

namespace NSW.Posts
{
    public partial class EditPostFailure : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SuccessMessage.Text = NSW.Data.LabelText.Text("AddPost.FailureMessage");
        }
    }
}