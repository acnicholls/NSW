using System;

namespace NSW.Posts
{
    public partial class ContactPostUserSuccess : PageBase
    {

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ContactSuccess.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ContactSuccess.Success");
        }
    }
}
