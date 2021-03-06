﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NSW.Admin
{
    public partial class LabelText : PageBase
    {
        SqlConnection labelConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
        SqlCommand labelComm;
        ListItem selectedItem = new ListItem(NSW.Data.LabelText.Text("SelectOne"), "NoValue");

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.Page_Load", "Here", LogEnum.Debug);
            if (!IsPostBack)
                LoadDropDownList();
            // load all text values
            this.LoadLabelValues();
        }

        /// <summary>
        /// validates form data and calls data operation method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.FinishClick", "Here", LogEnum.Debug);
            try
            {
                NSW.Data.LabelText lt = new NSW.Data.LabelText(this.ddlLabelID.SelectedItem.Text.ToString());
                lt.English = this.txtEnglish.Text.Trim();
                if (this.txtJapanese.Text.Length > 0)
                    lt.Japanese = this.txtJapanese.Text.Trim();
                else
                    lt.Japanese = string.Empty;
                lt.modifyLabel();
                this.LabelTextErrorMessage.Text = NSW.Data.LabelText.Text("LabelText.LabelEntrySuccess");
                selectedItem = new ListItem(NSW.Data.LabelText.Text("SelectOne"), "NoValue"); 
                LoadDropDownList();
                this.txtEnglish.Text = "";
                this.txtJapanese.Text = "";
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.FinishClick", x, LogEnum.Critical);
                this.LabelTextErrorMessage.Text = NSW.Data.LabelText.Text("LabelText.LabelEntryFailure");
            }
        }

        /// <summary>
        /// sends user to default page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelClick(object sender, EventArgs e)
        {
            // clear the form and return the user to the main webform
            string url = "~/Default.aspx";
            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        /// <summary>
        /// loads page data when drop down selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlLabelID_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItem = ddlLabelID.SelectedItem;
            try
            {
                // change the values of the form
                NSW.Data.LabelText lt = new Data.LabelText(selectedItem.Text);
                this.txtEnglish.Text = lt.English;
                this.txtJapanese.Text = lt.Japanese;
                // re-load the drop down list
                LoadDropDownList();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.ddlLabelID_SelectedIndexChanged", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// loads drop down data
        /// </summary>
        private void LoadDropDownList()
        {
            try
            {
                // load the dropdown values
                labelComm = labelConn.CreateCommand();
                labelComm.CommandType = CommandType.Text;
                labelComm.CommandText = "Select * from tblLabelText order by fldLabel_ID";
                DataSet ds = new DataSet();
                SqlDataAdapter adap = new SqlDataAdapter(labelComm);
                labelConn.Open();
                adap.Fill(ds);
                labelConn.Close();
                ddlLabelID.Items.Clear();
                ddlLabelID.Items.Add("Select One...");
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    // add the item
                    ddlLabelID.Items.Add(dr["fldLabel_ID"].ToString());
                    // if no japanese, color it red
                    if (dr["fldLabel_Japanese"].ToString() == string.Empty)
                        ddlLabelID.Items[i + 1].Attributes.Add("style", "color:red");
                }
                if (selectedItem.Value != "NoValue")
                {
                    ListItem chosenItem = ddlLabelID.Items.FindByText(selectedItem.Text);
                    chosenItem.Selected = true;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelText.LoadDropDownList", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// loads page data
        /// </summary>
        private void LoadLabelValues()
        {
            // now set the text values
            this.LabelTextTitle.Text = NSW.Data.LabelText.Text("LabelText.Title");
            this.LabelTextInstructions.Text = NSW.Data.LabelText.Text("LabelText.Instructions");
            this.lbl_ddlLabelID.Text = NSW.Data.LabelText.Text("LabelText.ddlLabelID");
            this.lbl_txtEnglish.Text = NSW.Data.LabelText.Text("LabelText.txtEnglish");
            this.lbl_txtJapanese.Text = NSW.Data.LabelText.Text("LabelText.txtJapanese");
            (Global.GetControlFromWizard(this.LabelTextWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("LabelText.FinishButton");
            this.EnglishRequired.Text = NSW.Data.LabelText.Text("LabelText.EnglishRequiredMessage");
        }
    }
}