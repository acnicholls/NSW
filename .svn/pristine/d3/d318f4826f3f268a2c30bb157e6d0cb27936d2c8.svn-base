using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSW.Posts
{
    public partial class EditPost : PageBase
    {
        NSW.Data.Post thisPost;
        string[] keyPairs;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                keyPairs = Global.GrabKeyPairs(Request.QueryString.ToString());
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.Page_Load", "Starting...", LogEnum.Debug);
                thisPost = new Data.Post(Convert.ToInt16(Global.KeyPairValue(keyPairs, "postID")));
                NSW.Data.User currUser = new Data.User(Page.User.Identity.Name);
                NSW.Data.User postUser = thisPost.PostUser();
                if (postUser.ID == currUser.ID)
                {
                    if (!IsPostBack)
                    {
                        LoadLabelValues();
                        LoadDropDownList();
                        LoadPostValues();
                    }
                }
                else
                {
                    Response.Redirect("~/Denied.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.Page_Load", x, LogEnum.Critical);
            }
        }

        private void LoadDropDownList()
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadDropDownList", "Starting...", LogEnum.Debug);
                // now the status drop down
                ListItem active = new ListItem(NSW.Data.LabelText.Text("PostStatus.Active"), "ACTIVE");
                ListItem sold = new ListItem(NSW.Data.LabelText.Text("PostStatus.Sold"), "SOLD");
                ListItem expired = new ListItem(NSW.Data.LabelText.Text("PostStatus.Expired"), "EXPIRED");
                this.PostStatus.Items.Add(active);
                this.PostStatus.Items.Add(sold);
                this.PostStatus.Items.Add(expired);
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadDropDownList", x, LogEnum.Critical);
            }
        }

        private void LoadPostValues()
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadPostValues", "Searching for : '" + thisPost.Status + "'", LogEnum.Debug);
                foreach (ListItem i in PostStatus.Items)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadPostValues", "current values : '" + i.Value + "'", LogEnum.Debug);
                }
                ListItem item = this.PostStatus.Items.FindByValue(thisPost.Status);
                item.Selected = true;
                this.PostTitle.Text = thisPost.Title;
                this.PostDescription.Text = thisPost.Description;
                this.PostPrice.Text = Convert.ToInt32(thisPost.Price).ToString();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadPostValues", x, LogEnum.Critical);
            }
        }

        private void LoadLabelValues()
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadLabelValues", "Starting...", LogEnum.Debug);
                this.EditPostPageTitle.Text = NSW.Data.LabelText.Text("EditPost.EditPostPageTitle");
                this.EditPostInstructions.Text = NSW.Data.LabelText.Text("EditPost.EditPostInstructions");
                // now the wizard controls
                (this.EditPostWizardStep0.FindControl("PostStatusLabel") as Label).Text = NSW.Data.LabelText.Text("EditPost.PostStatusLabel");
                (this.EditPostWizardStep0.FindControl("PostTitleLabel") as Label).Text = NSW.Data.LabelText.Text("EditPost.PostTitleLabel");
                (this.EditPostWizardStep0.FindControl("PostTitleRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("EditPost.PostTitleRequired");
                (this.EditPostWizardStep0.FindControl("PostDescriptionLabel") as Label).Text = NSW.Data.LabelText.Text("EditPost.PostDescriptionLabel");
                (this.EditPostWizardStep0.FindControl("PostDescriptionRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("EditPost.PostDescriptionRequired");
                (this.EditPostWizardStep0.FindControl("PostPriceLabel") as Label).Text = NSW.Data.LabelText.Text("EditPost.PostPriceLabel");
                (this.EditPostWizardStep0.FindControl("PostPriceRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("EditPost.PostPriceRequired");
                (Global.GetControlFromWizard(this.EditPostWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("EditPost.FinishButton");
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.LoadLabelValues", x, LogEnum.Critical);
            }
        }

        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.FinishClick", "Starting...", LogEnum.Debug);
                    thisPost.Description = this.PostDescription.Text.Trim();
                    thisPost.Title = this.PostTitle.Text.Trim();
                    thisPost.Price = Convert.ToDecimal(this.PostPrice.Text.Trim());
                    thisPost.Status = this.PostStatus.SelectedItem.Value;
                    thisPost.modifyPost();
                    Response.Redirect("EditPostSuccess.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "EditPost.FinishClick", x, LogEnum.Critical);
                Response.Redirect("EditPostFailure.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}