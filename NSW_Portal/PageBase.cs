﻿using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;


namespace NSW
{
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// decides which master page to display to user for mobile devices
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PageBase.OnPreInit", "Device Type (IsMobile) : " + IsMobileDevice.ToString(), LogEnum.Debug);
            if (IsMobileDevice)
            {
                MasterPageFile = "~/" + siteViewPreference + ".Master";
            }
        }

        /// <summary>
        /// checks to see if the user has a preference for which site view
        /// </summary>
        private string siteViewPreference
        {
            get
            {
                string returnValue = "";
                // figure out what the user wants
                HttpCookie version = Request.Cookies["VersionPref"];
                if (version != null)
                {
                    string preference = version.Value.ToString();
                    if (preference == "Desktop")
                        returnValue = "Site";
                    else if (preference == "Mobile")
                        returnValue = "Mobile";
                    else
                        returnValue = "Mobile";
                }
                else
                {
                    returnValue = "Mobile";
                }
                return returnValue;
            }
        }

        /// <summary>
        /// checks to see if site is being viewed in mobile or PC format
        /// </summary>
        public bool IsMobileView
        {
            get
            {
                if (IsMobileDevice)
                {
                    if (siteViewPreference == "Mobile")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// checks to see if device accessing site is mobile device or PC
        /// </summary>
        public bool IsMobileDevice
        {
            get
            {
                return Request.Browser.IsMobileDevice;
            }
        }

#if DEBUG
        /////// <summary>
        /////// Overridden to log the output for debugging
        /////// </summary>
        /////// <param name="writer"></param>
        ////protected override void Render(HtmlTextWriter writer)
        ////{
        ////    // *** Write the HTML into this string builder
        ////    StringBuilder sb = new StringBuilder();
        ////    StringWriter sw = new StringWriter(sb);

        ////    HtmlTextWriter hWriter = new HtmlTextWriter(sw);
        ////    base.Render(hWriter);

        ////    // *** store to a string
        ////    string PageResult = sb.ToString();

        ////    // Log the resulting string
        ////    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Page.Render", sb.ToString(), LogEnum.Debug);

        ////    // *** Write it back to the server
        ////    writer.Write(PageResult);
        ////}
#endif
    }
}