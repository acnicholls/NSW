﻿using System;
using System.IO;
using System.Web.UI;

namespace NSW.Posts
{
    public partial class ViewPost : PageBase
    {
        NSW.Data.Post thisPost;
        NSW.Data.User thisUser;
        NSW.Data.PostalCode userPC;
        string pFolder
        {
            get
            {
                    return "~/Posts/Photos/";
            }
        }

        string thisPostPhotoLocation
        {
            get
            {
                if (IsMobileDevice)
                    return pFolder + "/" + thisPost.ID.ToString() + "/Mobile/";
                else
                    return pFolder + "/" + thisPost.ID.ToString() + "/";

            }
        }
        string[] keyPairs;

        protected void Page_Load(object sender, EventArgs e)
        {
            keyPairs = Global.GrabKeyPairs(Request.QueryString.ToString());
            int itemID = Convert.ToInt32(Global.KeyPairValue(keyPairs, "postID"));
            LoadItemInfo(itemID);
            if (thisPost.IsActive)
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    NSW.Data.User currUser = new Data.User(Page.User.Identity.Name);
                    if (currUser.ID == thisUser.ID)
                        this.vpUserEdit.Visible = true;
                    else
                        this.vpUserEdit.Visible = false;
                }
                else
                    this.vpUserEdit.Visible = false;
                if (!IsPostBack)
                {
                    LoadLabelValues();
                    LoadItemPics(itemID);
                }
            }
            else
            {
                Response.Redirect("../Denied.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void vpItemThumb1_Click(object sender, ImageClickEventArgs e)
        {
            FileInfo[] picList = (FileInfo[])NSW.Data.Cache.Get("ItemPics");
            thisPost = (NSW.Data.Post)NSW.Data.Cache.Get("Post");
            this.vpItemPic.ImageUrl = thisPostPhotoLocation + picList[0].Name;
        }

        protected void vpItemThumb2_Click(object sender, ImageClickEventArgs e)
        {
            FileInfo[] picList = (FileInfo[])NSW.Data.Cache.Get("ItemPics");
            thisPost = (NSW.Data.Post)NSW.Data.Cache.Get("Post");
            this.vpItemPic.ImageUrl = thisPostPhotoLocation + picList[1].Name;
        }

        protected void vpItemThumb3_Click(object sender, ImageClickEventArgs e)
        {
            FileInfo[] picList = (FileInfo[])NSW.Data.Cache.Get("ItemPics");
            thisPost = (NSW.Data.Post)NSW.Data.Cache.Get("Post");
            this.vpItemPic.ImageUrl = thisPostPhotoLocation + picList[2].Name;
        }

        protected void vpItemThumb4_Click(object sender, ImageClickEventArgs e)
        {
            FileInfo[] picList = (FileInfo[])NSW.Data.Cache.Get("ItemPics");
            thisPost = (NSW.Data.Post)NSW.Data.Cache.Get("Post");
            this.vpItemPic.ImageUrl = thisPostPhotoLocation + picList[3].Name;
        }

        protected void vpUserContact_Click(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Global.KeyPairValue(keyPairs, "postID"));
            LoadItemInfo(itemID);
            Response.Redirect("ContactPostUser.aspx?func=list&postID=" + this.thisPost.ID.ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void vpUserItems_Click(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Global.KeyPairValue(keyPairs, "postID"));
            LoadItemInfo(itemID);
            Response.Redirect("PostList.aspx?func=list&userID=" + thisUser.ID.ToString(), false);
            Context.ApplicationInstance.CompleteRequest(); 
        }

        protected void vpUserEdit_Click(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Global.KeyPairValue(keyPairs, "postID"));
            LoadItemInfo(itemID);
            Response.Redirect("EditPost.aspx?postID=" + thisPost.ID.ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void LoadItemInfo(int itemID)
        {
            thisPost = new Data.Post(itemID);
            NSW.Data.Cache.Add("Post", thisPost);
            thisUser = thisPost.PostUser();
            userPC = new Data.PostalCode(thisUser.PostalCode);
        }

        private void LoadLabelValues()
        {
            this.vpDescription.Text = thisPost.Description;
            this.vpItemPrice.Text = NSW.Info.ProjectInfo.CurrencySymbol + Convert.ToInt32(thisPost.Price).ToString();
            this.vpItemTitle.Text = thisPost.Title;
            this.vpUserContact.Text = NSW.Data.LabelText.Text("ViewPost.vpUserContact");
            this.vpUserItems.Text = NSW.Data.LabelText.Text("ViewPost.vpUserItems");
            this.vpUserPhone.Text = NSW.Data.LabelText.Text("ViewPost.vpUserPhone");
            this.vpUserEdit.Text = NSW.Data.LabelText.Text("ViewPost.vpUserEdit");
            if (thisUser.Phone != string.Empty)
            {
                this.vpUserPhone.Text += " " + thisUser.Phone;
                this.vpUserPhone.Visible = true;
            }
            else
                this.vpUserPhone.Visible = false;

        }

        private void LoadItemPics(int itemID)
        {
            string Folder = thisPostPhotoLocation;
            DirectoryInfo picFolder = new DirectoryInfo(Server.MapPath(Folder));
            if (picFolder.Exists)
            {
                FileInfo[] pics = picFolder.GetFiles();
                switch (pics.Length)
                {
                    case 1:
                        {
                            this.vpItemThumb1.ImageUrl = Folder + "/" + pics[0].Name;
                            this.vpItemThumb2.Visible = false;
                            this.vpItemThumb3.Visible = false;
                            this.vpItemThumb4.Visible = false;
                            break;
                        }
                    case 2:
                        {
                            this.vpItemThumb1.ImageUrl = Folder + "/" + pics[0].Name;
                            this.vpItemThumb2.ImageUrl = Folder + "/" + pics[1].Name;
                            this.vpItemThumb3.Visible = false;
                            this.vpItemThumb4.Visible = false;
                            break;
                        }
                    case 3:
                        {
                            this.vpItemThumb1.ImageUrl = Folder + "/" + pics[0].Name;
                            this.vpItemThumb2.ImageUrl = Folder + "/" + pics[1].Name;
                            this.vpItemThumb3.ImageUrl = Folder + "/" + pics[2].Name;
                            this.vpItemThumb4.Visible = false;
                            break;
                        }
                    case 4:
                        {
                            this.vpItemThumb1.ImageUrl = Folder + "/" + pics[0].Name;
                            this.vpItemThumb2.ImageUrl = Folder + "/" + pics[1].Name;
                            this.vpItemThumb3.ImageUrl = Folder + "/" + pics[2].Name;
                            this.vpItemThumb4.ImageUrl = Folder + "/" + pics[3].Name;
                            break;
                        }
                }
                this.vpItemPic.ImageUrl = Folder + "/" + pics[0].Name;
                NSW.Data.Cache.Add("ItemPics", pics);
            }
            else
            {
                this.vpItemPic.Visible = false;
                this.vpItemThumb1.Visible = false;
                this.vpItemThumb2.Visible = false;
                this.vpItemThumb3.Visible = false;
                this.vpItemThumb4.Visible = false;
            }
        }

        public string LoadMap()
        {
            decimal lat = userPC.Latitude;
            decimal lon = userPC.Longitude;
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "ViewPost.LoadMap", "Lat:" + lat.ToString() + ", Long:" + lon.ToString(), LogEnum.Debug);
            string mapText = "<script type='text/javascript'>";
            mapText += " var map = L.map('map', { center:[";
            mapText += lat.ToString();
            mapText += ", " + lon.ToString();
            mapText += "], zoom:15});";
            mapText += "L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png'";
            mapText += ", {attribution: '&copy; <a href=\"http://osm.org/copyright\">OpenStreetMap</a> contributors'}";
            mapText += ").addTo(map);";
            mapText += "L.marker([" + lat.ToString() + ", " + lon.ToString() + "]).addTo(map);";
            mapText += "</script>";
            return mapText;
        }

    }
}