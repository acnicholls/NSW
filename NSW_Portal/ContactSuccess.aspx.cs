using System;

namespace NSW
{
    public partial class ContactSuccess : PageBase
    {

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle.Text = NSW.Data.LabelText.Text("ContactUsSuccess.Title");
            this.Success.Text = NSW.Data.LabelText.Text("ContactUsSuccess.Message");
        }
    }
}
