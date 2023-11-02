using System;

namespace NSW.Posts
{
    public partial class AddPostSuccess : PageBase
    {
        /// <summary>
        /// load page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SuccessMessage.Text = NSW.Data.LabelText.Text("AddPost.SuccessMessage");
        }
    }
}