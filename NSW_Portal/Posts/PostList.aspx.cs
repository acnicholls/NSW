﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace NSW.Posts
{
    public partial class PostList : PageBase
    {
        string[] keyPairs;
        string function = "";

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            keyPairs = Global.GrabKeyPairs(Request.QueryString.ToString());
            if (!IsPostBack)
            {
                LoadDropDownValues();
                LoadLabelValues();
                Session["PageNum"] = 1;
                LoadRepeater();
            }
            else
            {
                string target = Request.Params["__EVENTTARGET"].ToString();
                if (target != "")
                {
                    Control c = Page.FindControl(target);
                    if (c.ID == "ddlItemsPerPage")
                    {
                        Session["PageNum"] = 1;
                        LoadRepeater();
                    }
                }
            }
            SetNavButtons();
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostList.Page_Load", "PageNum : " + Session["PageNum"].ToString(), LogEnum.Debug);
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostList.Page_Load", "Sender : " + sender.ToString(), LogEnum.Debug);
        }

        /// <summary>
        /// sets visibility of page navigation buttons
        /// </summary>
        private void SetNavButtons()
        {
            if (Convert.ToInt32(Session["PageNum"]) == 1)
                this.btnPrev.Visible = false;
            else
                this.btnPrev.Visible = true;
            if (this.CheckDataSetLength() <= (Convert.ToInt32(this.ddlItemsPerPage.SelectedItem.Value) * Convert.ToInt32(Session["PageNum"])))
                this.btnNext.Visible = false;
            else
                this.btnNext.Visible = true;
        }
           
        /// <summary>
        /// loads dropdown data
        /// </summary>
                                                          
        private void LoadDropDownValues()
        {
            ListItem item = new ListItem("10", "10");
            this.ddlItemsPerPage.Items.Add(item);
            item.Selected = true;
            item = new ListItem("25", "25");
            this.ddlItemsPerPage.Items.Add(item);
            item = new ListItem("50", "50");
            this.ddlItemsPerPage.Items.Add(item);
            // newest to oldest
            item = new ListItem(NSW.Data.LabelText.Text("PostList.newToOld"), "fldPost_IntakeDate DESC");
            this.ddlSortOptions.Items.Add(item);
            item.Selected = true;
            // oldest to newest
            item = new ListItem(NSW.Data.LabelText.Text("PostList.oldToNew"), "fldPost_IntakeDate ASC");
            this.ddlSortOptions.Items.Add(item);
            // least to most
            item = new ListItem(NSW.Data.LabelText.Text("PostList.cheapToExp"), "fldPost_Price ASC");
            this.ddlSortOptions.Items.Add(item);
            // most to least
            item = new ListItem(NSW.Data.LabelText.Text("PostList.expToCheap"), "fldPost_Price DESC");
            this.ddlSortOptions.Items.Add(item);
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            this.ItemsPerPageLabel.Text = NSW.Data.LabelText.Text("PostList.ItemsPerPageLabel");
            this.SortOptionsLabel.Text = NSW.Data.LabelText.Text("PostList.SortOptionsLabel");
            this.btnNext.Text = NSW.Data.LabelText.Text("PostList.btnNext");
            this.btnPrev.Text = NSW.Data.LabelText.Text("PostList.btnPrev");
            if (Global.KeyPairValue(keyPairs, "func") == "list")
            {
                if (Global.KeyPairContains(keyPairs, "catID"))
                    this.CategoryName.Text = NSW.Data.PostCategory.Title(Convert.ToInt32(Request.QueryString["catID"]));
            }
            if(Global.KeyPairValue(keyPairs, "func") == "search")
                this.CategoryName.Text = NSW.Data.LabelText.Text("Search.SearchResults");
            this.emptyListLabel.Text = NSW.Data.LabelText.Text("PostList.EmptyList");
        }
        
        /// <summary>
        /// prepares variable values and sends call to database for dataset
        /// </summary>
        /// <param name="truncate">if true, truncate the section of SQL that removes unrequired rows</param>
        /// <returns>dataset of post rows</returns>

        private DataSet GrabSQLDataSet(bool truncate)
        {
            DataSet ds = new DataSet();
            try
            {
                // include request variable
                string limiter = "";
                function = Global.KeyPairValue(keyPairs, "func").Substring(0, 1);
                string catIDs = "";
                string term = "";
                int start = 0;
                int finish = 0;
                string sortOrder = "";
                int catid = 0;
                int userid = 0;
                switch (function)
                {
                    case "l":
                        {
                            if (Global.KeyPairContains(keyPairs, "catID"))
                            {
                                limiter = "c";
                                catid = Convert.ToInt32(Global.KeyPairValue(keyPairs, "catID"));
                            }
                            if (Global.KeyPairContains(keyPairs, "userID"))
                            {
                                limiter = "u";
                                userid = Convert.ToInt32(Global.KeyPairValue(keyPairs, "userID"));
                            }
                            break;
                        }
                    case "s":
                        {
                            catIDs = (string)NSW.Data.Cache.Get("SearchCategories");
                            term = (string)NSW.Data.Cache.Get("SearchTerm");
                            break;
                        }
                }
                ListItem numPerPage = this.ddlItemsPerPage.SelectedItem;
                start = 0;
                int curPage = 0;
                try
                {
                    curPage = Convert.ToInt32(Session["PageNum"]);
                }
                catch (NullReferenceException)
                {
                    curPage = 1;
                }
                if (curPage == 1)
                    start = 1;
                else
                {
                    int npp = Convert.ToInt32(numPerPage.Text);
                    start = (npp * (curPage - 1)) + 1;
                }
                finish = (start + Convert.ToInt32(numPerPage.Text));
                ListItem sort = this.ddlSortOptions.SelectedItem;
                sortOrder = sort.Value;
                // all paramters defined
                SqlConnection postConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand postComm = postConn.CreateCommand();
                postComm.CommandType = CommandType.StoredProcedure;
                postComm.CommandText = "searchPost";
                SqlParameter param = new SqlParameter("@term", term);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@func", function);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@limiter", limiter);
                postComm.Parameters.Add(param);
                if (catid > 0)
                    param = new SqlParameter("@catid", catid);
                else
                    param = new SqlParameter("@catid", -1);
                postComm.Parameters.Add(param);
                if (userid > 0)
                    param = new SqlParameter("@userid", userid);
                else
                    param = new SqlParameter("@userid", -1);
                postComm.Parameters.Add(param);
                if (catIDs != null)
                    param = new SqlParameter("@searchCats", catIDs);
                else
                    param = new SqlParameter("@searchCats", "");
                postComm.Parameters.Add(param);
                param = new SqlParameter("@trunc", truncate);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@start", start);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@finish", finish);
                postComm.Parameters.Add(param);
                param = new SqlParameter("@order", sortOrder);
                postComm.Parameters.Add(param);
                SqlDataAdapter adap = new SqlDataAdapter(postComm);
                postConn.Open();
                adap.Fill(ds);
                postConn.Close();
                return ds;
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostList.GrabSQLDataSet", x, LogEnum.Critical);
            }
            return null;
        }

        /// <summary>
        /// loads dataset values into page repeater
        /// </summary>
        private void LoadRepeater()
        {
            try
            {
                DataSet ds = this.GrabSQLDataSet(false);
                if (function == "search" && ds.Tables[0].Rows.Count == 0)
                {
                    this.rptPostList.Visible = false;
                    this.emptyListLabel.Visible = true;
                }

                this.rptPostList.DataSource = ds.Tables[0];
                this.rptPostList.DataBind();
                foreach (RepeaterItem item in rptPostList.Items)
                {
                    int postID = Convert.ToInt32(ds.Tables[0].Rows[item.ItemIndex]["fldPost_id"]);
                    // find the controls and place the data in them
                    Image pic = (Image)item.FindControl("PostImage");
                    pic.ImageUrl = GetPostPhoto(postID);
                    HyperLink link = (HyperLink)item.FindControl("PostTitleButton");
                    link.NavigateUrl = "~/Posts/ViewPost.aspx?postID=" + postID.ToString();
                    link.Text = ds.Tables[0].Rows[item.ItemIndex]["fldPost_Title"].ToString();
                    HyperLink imgLink = (HyperLink)item.FindControl("ImageLink");
                    imgLink.NavigateUrl = link.NavigateUrl;
                    Label price = (Label)item.FindControl("PostPrice");
                    price.Text = NSW.Info.ProjectInfo.CurrencySymbol + Convert.ToInt32(ds.Tables[0].Rows[item.ItemIndex]["fldPost_Price"]).ToString();
                    Label desc = (Label)item.FindControl("PostDescription");
                    string description = ds.Tables[0].Rows[item.ItemIndex]["fldPost_Description"].ToString();
                    if (IsMobileView)
                    {
                        if (description.Length > 50)
                            desc.Text = description.Substring(0, 50);
                        else
                            desc.Text = description;
                    }
                    else
                    {
                        if (description.Length > 300)
                            desc.Text = description.Substring(0, 300);
                        else
                            desc.Text = description;
                    }
                    if (IsMobileDevice)
                        desc.Text += "..... ";
                    else
                        desc.Text += "..... " + NSW.Data.LabelText.Text("ClickForMore");

                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostList.GrabDataSet", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// acquires file name of post photo
        /// </summary>
        /// <param name="ID">ID value of post</param>
        /// <returns>relative filename of post's first photo</returns>
        private string GetPostPhoto(int ID)
        {
            DirectoryInfo PhotoFolder = new DirectoryInfo(Server.MapPath("~/Posts/Photos/" + ID.ToString()));
            if (PhotoFolder.Exists)
            {
                FileInfo[] pics = PhotoFolder.GetFiles();
                if (pics.Length > 0)
                {
                        return "Photos/" + ID.ToString() + "/" + pics[0].Name;
                }
                else
                    return "~/images/noPic.JPG";
            }
            else
                return "~/images/noPic.JPG";
        }

        /// <summary>
        /// loads previous page of posts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int PageNum = Convert.ToInt32(Session["PageNum"]);
            Session["PageNum"] = PageNum - 1;
            LoadRepeater();
            SetNavButtons();
        }

        /// <summary>
        /// loads next page of posts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            int PageNum = Convert.ToInt32(Session["PageNum"]);
            Session["PageNum"] = PageNum + 1;
            LoadRepeater();
            SetNavButtons();
        }

        /// <summary>
        /// performs page operations after dropdown selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlItemsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRepeater();
        }

        /// <summary>
        /// performs page operations after dropdown selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSortOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRepeater();
        }

        /// <summary>
        /// calls complete dataset to return total length
        /// </summary>
        /// <returns>number of datarows</returns>
        private int CheckDataSetLength()
        {
            try
            {
                DataSet ds = GrabSQLDataSet(true);
                return ds.Tables[0].Rows.Count;
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostList.CheckDataSetLength", x, LogEnum.Critical);
            }
            return 0;
        }
    }
}