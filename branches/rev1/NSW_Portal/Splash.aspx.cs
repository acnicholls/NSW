﻿using System;
using System.Web;

namespace NSW
{
    public partial class Splash : System.Web.UI.Page
    {
        private DateTime today = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Splash.Page_Load", "Starting...", LogEnum.Debug);
            NSW.Data.LabelText welcome = new Data.LabelText("Splash.Welcome");
            this.SplashWelcomeEnglish.Text = welcome.English;
            this.SplashWelcomeJapanese.Text = welcome.Japanese;
            NSW.Data.LabelText instructions = new Data.LabelText("Splash.Instructions");
            this.SplashInstructionsEnglish.Text = instructions.English;
            this.SplashInstructionsJapanese.Text = instructions.Japanese;
        }

        protected void btnEnglish_Click(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Splash.btnEnglish_Click", "Starting...", LogEnum.Debug);
            Session["DisplayLanguage"] = "English";
            HttpCookie langCook = new HttpCookie("LanguageCookie", "English");
            langCook.Expires = DateTime.MaxValue;
            Response.Cookies.Add(langCook);
            Response.Redirect("~/Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnJapanese_Click(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Splash.btnEnglish_Click", "Starting...", LogEnum.Debug);
            Session["DisplayLanguage"] = "Japanese";
            HttpCookie langCook = new HttpCookie("LanguageCookie", "Japanese");
            langCook.Expires = DateTime.MaxValue;
            Response.Cookies.Add(langCook);
            Response.Redirect("~/Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }


    }
}