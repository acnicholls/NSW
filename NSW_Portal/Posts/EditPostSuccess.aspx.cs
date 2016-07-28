﻿using System;

namespace NSW.Posts
{
    public partial class EditPostSuccess : PageBase
    {

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SuccessMessage.Text = NSW.Data.LabelText.Text("EditPost.SuccessMessage");
        }
    }
}