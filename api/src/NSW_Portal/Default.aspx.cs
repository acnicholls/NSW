using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NSW
{
    public partial class _Default : PageBase
    {
        private SqlConnection defConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
        private SqlCommand defComm;
        private DataTable ButtonList = new DataTable("CategoryList");
        private DataColumn CategoryTitle = new DataColumn("CategoryTitle");
        private DataColumn CategoryDesc = new DataColumn("CategoryDescription");
        private DataColumn CategoryID = new DataColumn("CategoryID");
        private DataColumn CategoryPosts = new DataColumn("CategoryPosts");
        private DataTable ButtonListLeft = new DataTable("CategoryList");
        private DataColumn CategoryTitleLeft = new DataColumn("CategoryTitle");
        private DataColumn CategoryDescLeft = new DataColumn("CategoryDescription");
        private DataColumn CategoryIDLeft = new DataColumn("CategoryID");
        private DataColumn CategoryPostsLeft = new DataColumn("CategoryPosts");
        private DataTable ButtonListRight = new DataTable("CategoryList");
        private DataColumn CategoryTitleRight = new DataColumn("CategoryTitle");
        private DataColumn CategoryDescRight = new DataColumn("CategoryDescription");
        private DataColumn CategoryIDRight = new DataColumn("CategoryID");
        private DataColumn CategoryPostsRight = new DataColumn("CategoryPosts");

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadLabelValues();
                    ButtonList.Columns.Add(CategoryID);
                    ButtonList.Columns.Add(CategoryTitle);
                    ButtonList.Columns.Add(CategoryDesc);
                    ButtonList.Columns.Add(CategoryPosts);
                    // now get the info
                    defComm = defConn.CreateCommand();
                    defComm.CommandType = CommandType.Text;
                    defComm.CommandText = "Select * from tblPostCategories";
                    SqlDataAdapter adap = new SqlDataAdapter(defComm);
                    DataSet ds = new DataSet();
                    defConn.Open();
                    adap.Fill(ds);
                    defConn.Close();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        // grab a count of posts for the category
                        defComm.CommandText = "Select count(*) from tblPosts where fldPost_CategoryID=" + dr["fldPostCategory_id"].ToString() + " and fldPost_Status not in ('SOLD','EXPIRED')";
                        defConn.Open();
                        object numOfPosts = defComm.ExecuteScalar();
                        defConn.Close();
                        DataRow newRow = ButtonList.NewRow();
                        int id = Convert.ToInt32(dr["fldPostCategory_id"]);
                        newRow["CategoryID"] = id;
                        newRow["CategoryTitle"] = NSW.Data.PostCategory.Title(id); 
                        newRow["CategoryDescription"] = NSW.Data.PostCategory.Description(id);
                        newRow["CategoryPosts"] = numOfPosts.ToString();
                        ButtonList.Rows.Add(newRow);
                        ButtonList.AcceptChanges();
                    }
                    NSW.Data.Cache.Add("ButtonList", ButtonList);
                    // bind the data to the repeater
                    this.splitButtonList();
                    BindButtonListLeft();
                    BindButtonListRight();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Default.Page_Load", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// sends user to the post list for the selected category
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void CategoryList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Launch")
            {
                int id = Convert.ToInt32((e.Item.FindControl("CategoryID") as HiddenField).Value);
                Response.Redirect("~/Posts/PostList.aspx?func=list&catID=" + id.ToString(), false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        /// <summary>
        /// loads repeater values for list on left of page
        /// </summary>
        private void BindButtonListLeft()
        {
            this.CategoryListLeft.DataSource = ButtonListLeft;
            this.CategoryListLeft.DataBind();
            foreach (RepeaterItem item in CategoryListLeft.Items)
            {
                HiddenField hidden = (HiddenField)item.FindControl("CategoryID");
                hidden.Value = ButtonListLeft.Rows[item.ItemIndex]["CategoryID"].ToString();
                Button but = (Button)item.FindControl("CategoryButton");
                but.Text = ButtonListLeft.Rows[item.ItemIndex]["CategoryTitle"].ToString();
                Label lab = (Label)item.FindControl("CategoryDescription");
                lab.Text = ButtonListLeft.Rows[item.ItemIndex]["CategoryDescription"].ToString();
                Label posts = (Label)item.FindControl("CategoryPosts");
                posts.Text = ButtonListLeft.Rows[item.ItemIndex]["CategoryPosts"].ToString();
            }
        }

        /// <summary>
        /// loads repeater values for list on right of page
        /// </summary>
        private void BindButtonListRight()
        {
            this.CategoryListRight.DataSource = ButtonListRight;
            this.CategoryListRight.DataBind();
            foreach (RepeaterItem item in CategoryListRight.Items)
            {
                HiddenField hidden = (HiddenField)item.FindControl("CategoryID");
                hidden.Value = ButtonListRight.Rows[item.ItemIndex]["CategoryID"].ToString();
                Button but = (Button)item.FindControl("CategoryButton");
                but.Text = ButtonListRight.Rows[item.ItemIndex]["CategoryTitle"].ToString();
                Label lab = (Label)item.FindControl("CategoryDescription");
                lab.Text = ButtonListRight.Rows[item.ItemIndex]["CategoryDescription"].ToString();
                Label posts = (Label)item.FindControl("CategoryPosts");
                posts.Text = ButtonListRight.Rows[item.ItemIndex]["CategoryPosts"].ToString();
            }
        }

        /// <summary>
        /// splits data values into two parts for left list and right list
        /// </summary>
        private void splitButtonList()
        {
            try
            {
                // build the two tables
                // left
                ButtonListLeft.Columns.Add(this.CategoryIDLeft);
                ButtonListLeft.Columns.Add(this.CategoryTitleLeft);
                ButtonListLeft.Columns.Add(this.CategoryDescLeft);
                ButtonListLeft.Columns.Add(this.CategoryPostsLeft);
                // right
                ButtonListRight.Columns.Add(this.CategoryIDRight);
                ButtonListRight.Columns.Add(this.CategoryTitleRight);
                ButtonListRight.Columns.Add(this.CategoryDescRight);
                ButtonListRight.Columns.Add(this.CategoryPostsRight);
                // now split the list between the two lists.
                for (int i = 0; i <= ButtonList.Rows.Count - 1; i = i + 2)
                {
                    DataRow leftRow = ButtonListLeft.NewRow();
                    leftRow[0] = ButtonList.Rows[i][0];
                    leftRow[1] = ButtonList.Rows[i][1];
                    leftRow[2] = ButtonList.Rows[i][2];
                    leftRow[3] = ButtonList.Rows[i][3];
                    ButtonListLeft.Rows.Add(leftRow);
                    ButtonListLeft.AcceptChanges();
                    if (ButtonList.Rows.Count > (i+1))
                    {
                        DataRow rightRow = ButtonListRight.NewRow();
                        rightRow[0] = ButtonList.Rows[i + 1][0];
                        rightRow[1] = ButtonList.Rows[i + 1][1];
                        rightRow[2] = ButtonList.Rows[i + 1][2];
                        rightRow[3] = ButtonList.Rows[i + 1][3];
                        ButtonListRight.Rows.Add(rightRow);
                        ButtonListRight.AcceptChanges();
                    }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Default.splitButtonList", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            this.Instructions.Text = NSW.Data.LabelText.Text("Main.Instructions");
        }
    }
}
