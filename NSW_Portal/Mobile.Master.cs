﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSW
{
    public partial class MobileMaster : System.Web.UI.MasterPage
    {
        public string SplashLink = NSW.Info.ProjectInfo.protocol + NSW.Info.AppSettings.GetAppSetting("webServer", false);

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Master.Page_Load", "Starting...", LogEnum.Debug);
            setText();
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void setText()
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Master.setText", "Starting...", LogEnum.Debug);
            // now set all the label text
            // switch text
            HttpCookie version = Request.Cookies["VersionPref"];
            if (version != null)
            {
                string reqVersion = version.Value.ToString();
                if (reqVersion == "Desktop")
                    lnkSwitch.Text = NSW.Data.LabelText.Text("Master.lnkSwitchDesktop");
                else if (reqVersion == "Mobile")
                    lnkSwitch.Text = NSW.Data.LabelText.Text("Master.lnkSwitchMobile");
            }
            else
                lnkSwitch.Text = NSW.Data.LabelText.Text("Master.lnkSwitchMobile");
            // title of the page
            lblTitle.Text = NSW.Data.LabelText.Text("Master.lblTitle");
            // Title bar button "Login"
            Label newLabel = (Label)this.HeadLoginView.FindControl("lblLogin");
            if (newLabel != null)
                newLabel.Text = NSW.Data.LabelText.Text("Master.lblLogin");
            // register button
            Label registerLabel = (Label)this.HeadLoginView.FindControl("lblRegister");
            if (registerLabel != null)
                registerLabel.Text = NSW.Data.LabelText.Text("Login.RegisterHyperLink");
            // logout button
            if (Page.User.Identity.IsAuthenticated)
            {
                LoginStatus newStatus = (LoginStatus)this.HeadLoginView.FindControl("HeadLoginStatus");
                if (newStatus != null)
                    newStatus.LogoutText = NSW.Data.LabelText.Text("Master.lblLogout");
            }
            // Menu bar
            MenuItem searchItem = new MenuItem(NSW.Data.LabelText.Text("Master.btnSearch"), null, null, "~/Search.aspx");
            MenuItem homeItem = new MenuItem(NSW.Data.LabelText.Text("Master.btnHome"), null, null, "~/Default.aspx");
            MenuItem contactItem = new MenuItem(NSW.Data.LabelText.Text("Master.btnContact"), null, null, "~/Contact.aspx");
            this.NavigationMenu.Items.Clear();
            this.NavigationMenu.Items.Add(homeItem);
            this.NavigationMenu.Items.Add(searchItem);
            this.NavigationMenu.Items.Add(contactItem);
            if (Page.User.Identity.IsAuthenticated)
            {
                MenuItem addItem = new MenuItem(NSW.Data.LabelText.Text("Master.btnAdd"), null, null, "~/Posts/AddPost.aspx");
                Data.User curUser = new Data.User(Page.User.Identity.Name);
                MenuItem myPosts = new MenuItem(NSW.Data.LabelText.Text("Master.btnMy"), null, null, "~/Posts/PostList.aspx?func=list&userID=" + curUser.ID.ToString());
                this.NavigationMenu.Items.Add(addItem);
                this.NavigationMenu.Items.Add(myPosts);
            }
            // footer
            this.lblFooter.Text = NSW.Data.LabelText.Text("Master.lblFooter");
        }

        /// <summary>
        /// switches user between desktop and mobile view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSwitch_Click(object sender, EventArgs e)
        {
            HttpCookie version = new HttpCookie("VersionPref", "Desktop");
            Response.Cookies.Add(version);
            string url = Request.RawUrl;
            Response.Redirect(url);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
