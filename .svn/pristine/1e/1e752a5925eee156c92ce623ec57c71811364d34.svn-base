using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NSW
{
    public partial class Search : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NSW.Data.Cache.Remove("SearchCategories");
                NSW.Data.Cache.Remove("SearchTerm");
                LoadLabelValues();
                LoadListBox();
            }
        }

        private void LoadLabelValues()
        {
            this.SearchPageTitle.Text = NSW.Data.LabelText.Text("Search.SearchPageTitle");
            this.SearchInstructions.Text = NSW.Data.LabelText.Text("Search.SearchInstructions");
            (this.SearchWizardStep0.FindControl("SearchCategoryPickerLabel") as Label).Text = NSW.Data.LabelText.Text("Search.SearchCategoryPickerLabel");
            (this.SearchWizardStep0.FindControl("SearchCategoryPickerLabel1") as Label).Text = NSW.Data.LabelText.Text("Search.SearchCategoryPickerLabel1");
            (this.SearchWizardStep0.FindControl("SearchTermLabel") as Label).Text = NSW.Data.LabelText.Text("Search.SearchTermLabel");
            (Global.GetControlFromWizard(this.SearchWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("Search.FinishButton");
        }

        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            if (this.SearchTerm.Text.Length > 0)
            {
                string strCat = "";
                if (SearchCategoryPicker.SelectedIndex > -1)
                {
                    foreach (ListItem i in SearchCategoryPicker.Items)
                    {
                        if (i.Selected)
                            strCat += i.Value + ",";
                    }
                    strCat = strCat.Substring(0, strCat.Length - 1);
                }
                NSW.Data.Cache.Add("SearchCategories", strCat);
                if (this.SearchTerm.Text.Length > 0)
                {
                    NSW.Data.Cache.Add("SearchTerm", this.SearchTerm.Text.Trim());
                }
                Response.Redirect("~/Posts/PostList.aspx?func=search", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                this.SearchErrorMessage.Text = NSW.Data.LabelText.Text("Search.SearchErrorMessage");
                e.Cancel = true;
            }
        }

        private void LoadListBox()
        {
            SqlConnection searchConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            SqlCommand searchComm = searchConn.CreateCommand();
            searchComm.CommandType = CommandType.Text;
            searchComm.CommandText = "Select * from tblPostCategories order by fldPostCategory_id";
            SqlDataAdapter adap = new SqlDataAdapter(searchComm);
            DataSet ds = new DataSet();
            searchConn.Open();
            adap.Fill(ds);
            searchConn.Close();
            this.SearchCategoryPicker.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                NSW.Data.PostCategory cat = new Data.PostCategory(Convert.ToInt32(dr["fldPostCategory_id"]));
                ListItem item = new ListItem(NSW.Data.PostCategory.Title(cat.ID), cat.ID.ToString());
                this.SearchCategoryPicker.Items.Add(item);
            }
        }
    }
}
