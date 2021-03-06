﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using NSW.Data;


namespace NSW.Admin
{
    public partial class Categories : PageBase
    {

        NSW.Data.PostCategory category = new PostCategory();
        ListItem selectedItem = new ListItem(NSW.Data.LabelText.Text("SelectOne"), "NoValue");

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLabelValues();
            if(!IsPostBack)
                LoadDropDownList();
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            // page instruction labels
            this.CategoryPageTitle.Text = NSW.Data.LabelText.Text("Category.Title");
            this.CategoryInstructions.Text = NSW.Data.LabelText.Text("Category.Instructions");
            this.CategoryInstructions2.Text = NSW.Data.LabelText.Text("Category.CategoryInstructions2");
            // now the wizard controls
            (this.CategoryWizardStep0.FindControl("CategoryPickerLabel") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryPickerLabel");
            (this.CategoryWizardStep0.FindControl("CategoryTitle") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryTitle");
            (this.CategoryWizardStep0.FindControl("CategoryEnglishLabel") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryEnglishLabel");
            (this.CategoryWizardStep0.FindControl("CategoryJapaneseLabel") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryJapaneseLabel");
            (this.CategoryWizardStep0.FindControl("CategoryDescription") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryDescription");
            (this.CategoryWizardStep0.FindControl("CategoryEnglishDescriptionLabel") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryEnglishDescriptionLabel");
            (this.CategoryWizardStep0.FindControl("CategoryJapaneseDescriptionLabel") as Label).Text = NSW.Data.LabelText.Text("Category.CategoryJapaneseDescriptionLabel");
            (Global.GetControlFromWizard(this.CategoryWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("Category.FinishButton");
        }

        /// <summary>
        /// performs data operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                PostCategory OldCategory;
                if (CategoryPicker.SelectedItem.Text == "Select One...")
                    OldCategory = new PostCategory();
                else
                    OldCategory = new PostCategory(Convert.ToInt32(CategoryPicker.SelectedValue));
                // send values to data object 
                OldCategory.EnglishTitle = this.CategoryEnglish.Text.Trim();
                OldCategory.EnglishDescription = this.CategoryEnglishDescription.Text.Trim();
                OldCategory.JapaneseTitle = this.CategoryJapanese.Text.Trim();
                OldCategory.JapaneseDescription = this.CategoryJapaneseDescription.Text.Trim();
                // send values to database
                if (OldCategory.ID == 0)
                    OldCategory.insertCategory();
                else
                    OldCategory.modifyCategory();
                this.CategoryErrorMessage.Text = NSW.Data.LabelText.Text("Category.SuccessMessage");
                ClearForm();
                LoadDropDownList();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Categories.FinishClick", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// loads new page data when drop down selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItem = CategoryPicker.SelectedItem;
            if (selectedItem.Text != "Select One...")
            {
                this.category = new PostCategory(Convert.ToInt32(selectedItem.Value));
                this.CategoryEnglish.Text = category.EnglishTitle;
                this.CategoryEnglishDescription.Text = category.EnglishDescription;
                this.CategoryJapanese.Text = category.JapaneseTitle;
                this.CategoryJapaneseDescription.Text = category.JapaneseDescription;
            }
            else
                ClearForm();
        }

        /// <summary>
        /// loads drop down data
        /// </summary>
        private void LoadDropDownList()
        {
            try
            {
                SqlConnection ddConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                SqlCommand ddComm = ddConn.CreateCommand();
                ddComm.CommandType = CommandType.Text;
                ddComm.CommandText = "Select fldPostCategory_id, fldPostCategory_English from tblPostCategories";
                SqlDataAdapter adap = new SqlDataAdapter(ddComm);
                DataSet ds = new DataSet();
                ddConn.Open();
                adap.Fill(ds);
                ddConn.Close();
                // use the dataset to fill the drop down
                this.CategoryPicker.Items.Clear();
                ListItem selectOne = new ListItem(NSW.Data.LabelText.Text("SelectOne"), "NoValue");
                this.CategoryPicker.Items.Add(selectOne);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem item = new ListItem(dr["fldPostCategory_English"].ToString(), dr["fldPostCategory_id"].ToString());
                    this.CategoryPicker.Items.Add(item);
                }
                if (selectedItem.Text != "Select One...")
                {
                    ListItem chosenItem = CategoryPicker.Items.FindByValue(selectedItem.Value);
                    chosenItem.Selected = true;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Categories.LoadDropDownList", x, LogEnum.Critical);
            }
        
        }

        /// <summary>
        /// clears form 
        /// </summary>
        private void ClearForm()
        {
            this.CategoryEnglish.Text = "";
            this.CategoryEnglishDescription.Text = "";
            this.CategoryJapanese.Text = "";
            this.CategoryJapaneseDescription.Text = "";
            this.selectedItem = new ListItem(NSW.Data.LabelText.Text("SelectOne"), "NoValue");
        }
    }
}