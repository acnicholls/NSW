using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace NSW.Posts
{
    public partial class AddPost : PageBase
    {
        DirectoryInfo photoFolder;
        DirectoryInfo mobileFolder;

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.Page_Load", "Starting...", LogEnum.Debug);
            if (!IsPostBack)
            {
                LoadLabelValues();
                LoadDropDownList();
            }
        }

        /// <summary>
        /// loads drop down data
        /// </summary>
        private void LoadDropDownList()
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.LoadDropDownList", "Starting...", LogEnum.Debug);
            SqlConnection catConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
            SqlCommand catComm = catConn.CreateCommand();
            catComm.CommandType = CommandType.Text;
            catComm.CommandText = "Select fldPostCategory_id from tblPostCategories order by fldPostCategory_id";
            SqlDataAdapter adap = new SqlDataAdapter(catComm);
            DataSet ds = new DataSet();
            catConn.Open();
            adap.Fill(ds);
            catConn.Close();
            ListItem selectOne = new ListItem(NSW.Data.LabelText.Text("SelectOne"), "NoValue");
            this.PostCategoryPicker.Items.Add(selectOne);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                ListItem item = new ListItem(NSW.Data.PostCategory.Title(Convert.ToInt32(dr["fldPostCategory_id"])), dr["fldPostCategory_id"].ToString());
                this.PostCategoryPicker.Items.Add(item);
            }
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.LoadLabelValues", "Starting...", LogEnum.Debug);
            this.AddPostPageTitle.Text = NSW.Data.LabelText.Text("AddPost.AddPostPageTitle");
            this.AddPostInstructions.Text = NSW.Data.LabelText.Text("AddPost.AddPostInstructions");
            // now the wizard controls
            (this.AddPostWizardStep0.FindControl("PostCategoryPickerLabel") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostCategoryPickerLabel");
            (this.AddPostWizardStep0.FindControl("PostCategoryRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("AddPost.PostCategoryRequired");
            (this.AddPostWizardStep0.FindControl("PostTitleLabel") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostTitleLabel");
            (this.AddPostWizardStep0.FindControl("PostTitleRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("AddPost.PostTitleRequired");
            (this.AddPostWizardStep0.FindControl("PostDescriptionLabel") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostDescriptionLabel");
            (this.AddPostWizardStep0.FindControl("PostDescriptionRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("AddPost.PostDescriptionRequired");
            (this.AddPostWizardStep0.FindControl("PostPriceLabel") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostPriceLabel");
            (this.AddPostWizardStep0.FindControl("PostPriceRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("AddPost.PostPriceRequired");
            (this.AddPostWizardStep0.FindControl("PostPhoto1FileContent") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostPhotoErrorMessage");
            (this.AddPostWizardStep0.FindControl("PostPhoto2FileContent") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostPhotoErrorMessage");
            (this.AddPostWizardStep0.FindControl("PostPhoto3FileContent") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostPhotoErrorMessage");
            (this.AddPostWizardStep0.FindControl("PostPhoto4FileContent") as Label).Text = NSW.Data.LabelText.Text("AddPost.PostPhotoErrorMessage");
            (Global.GetControlFromWizard(this.AddPostWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("AddPost.FinishButton");
        }

        /// <summary>
        /// validates form data, calls data operation method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", "Starting...", LogEnum.Debug);
                bool valid = true;
                if (this.PostCategoryPicker.SelectedIndex == -1)
                {
                    this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.PostCategoryRequired");
                    valid = false;
                }
                this.Captcha1.ValidateCaptcha(this.txtCaptcha.Text.Trim().ToLower());
                if (!Captcha1.UserValidated)
                {
                    this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.BadCaptcha");
                    valid = false;
                }
                if (!IsNumber(this.PostPrice.Text))
                {
                    this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.PriceNumber");
                    valid = false;
                }
                if (this.PostPhoto1.HasFile)
                {
                    if (!IsFileValid(this.PostPhoto1))
                        valid = false;
                }
                if (this.PostPhoto2.HasFile)
                {
                    if (!IsFileValid(this.PostPhoto2))
                        valid = false;
                }
                if (this.PostPhoto3.HasFile)
                {
                    if (!IsFileValid(this.PostPhoto3))
                        valid = false;
                }
                if (this.PostPhoto4.HasFile)
                {
                    if (!IsFileValid(this.PostPhoto4))
                        valid = false;
                }

                if (valid)
                {
                    NSW.Data.Post newPost = new Data.Post();
                    newPost.CategoryID = Convert.ToInt32(this.PostCategoryPicker.SelectedItem.Value);
                    newPost.Description = this.PostDescription.Text.Trim();
                    newPost.Title = this.PostTitle.Text.Trim();
                    newPost.Price = Convert.ToDecimal(this.PostPrice.Text.Trim());
                    Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", "UserName : " + Page.User.Identity.Name.ToString(), LogEnum.Debug);
                    NSW.Data.User postUser = new Data.User(Page.User.Identity.Name);
                    newPost.UserID = postUser.ID;
                    newPost.insertPost();
                    // now add photos to Photo folder
                    if (this.PostPhoto1.HasFile || this.PostPhoto2.HasFile || this.PostPhoto3.HasFile || this.PostPhoto4.HasFile)
                    {
                        DirectoryInfo folder = new DirectoryInfo(Server.MapPath("~/Posts/Photos"));
                        photoFolder = folder.CreateSubdirectory(newPost.ID.ToString());
                        mobileFolder = photoFolder.CreateSubdirectory("Mobile");
                    }
                    if (this.PostPhoto1.HasFile)
                    {
                        string filename = "";
                        if(IsMobileDevice)
                         filename = "1" + this.PostPhoto1.FileName;
                        else
                         filename = this.PostPhoto1.FileName;

                        Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", "Saving file 1 as : " + filename, LogEnum.Debug);
                        // save photo and mobile version
                        this.PostPhoto1.SaveAs(photoFolder.FullName + @"\" + filename);
                        Bitmap photo = new Bitmap(this.PostPhoto1.FileContent);
                        string mobile_Filename = mobileFolder.FullName + @"\" + filename;
                        OptimizeNResize(photo, mobile_Filename);
                    }
                    if (this.PostPhoto2.HasFile)
                    {
                        string filename = "";
                        if (IsMobileDevice)
                            filename = "2" + this.PostPhoto2.FileName;
                        else
                            filename = this.PostPhoto2.FileName;
                        Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", "Saving file 2 as : " + filename, LogEnum.Debug);
                        // save photo and mobile version
                        this.PostPhoto2.SaveAs(photoFolder.FullName + @"\" + filename);
                        Bitmap photo = new Bitmap(this.PostPhoto2.FileContent);
                        string mobile_Filename = mobileFolder.FullName + @"\" + filename;
                        OptimizeNResize(photo, mobile_Filename);
                    }
                    if (this.PostPhoto3.HasFile)
                    {
                        string filename = "";
                        if (IsMobileDevice)
                            filename = "3" + this.PostPhoto3.FileName;
                        else
                            filename = this.PostPhoto3.FileName;
                        Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", "Saving file 3 as : " + filename, LogEnum.Debug);

                        this.PostPhoto3.SaveAs(photoFolder.FullName + @"\" + filename);
                        Bitmap photo = new Bitmap(this.PostPhoto3.FileContent);
                        string mobile_Filename = mobileFolder.FullName + @"\" + filename;
                        OptimizeNResize(photo, mobile_Filename);
                    }
                    if (this.PostPhoto4.HasFile)
                    {
                        string filename = "";
                        if (IsMobileDevice)
                            filename = "4" + this.PostPhoto4.FileName;
                        else
                            filename = this.PostPhoto4.FileName;
                        Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", "Saving file 4 as : " + filename, LogEnum.Debug);

                        this.PostPhoto4.SaveAs(photoFolder.FullName + @"\" + filename);
                        Bitmap photo = new Bitmap(this.PostPhoto4.FileContent);
                        string mobile_Filename = mobileFolder.FullName + @"\" + filename;
                        OptimizeNResize(photo, mobile_Filename);
                    }
                    if (newPost.ID > 0)
                        Response.Redirect("AddPostSuccess.aspx", false);
                    else
                        Response.Redirect("AddPostFailure.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.FinishClick", x, LogEnum.Debug);
                Response.Redirect("AddPostFailure.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        /// <summary>
        /// validates file selected for upload
        /// </summary>
        /// <param name="itemToCheck"></param>
        /// <returns></returns>
        private bool IsFileValid(FileUpload itemToCheck)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.IsFileValid", "Starting...", LogEnum.Debug);

            bool valid = true;
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.IsFileValid", "Photo File Length is : " + itemToCheck.FileContent.Length.ToString(), LogEnum.Message);

            if (itemToCheck.FileContent.Length > 4194304)
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.4MegMessage");
            }
            if (itemToCheck.FileName.Contains("*"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains("&"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains("%"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains("\\"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains("?"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains(":"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains("<"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            if (itemToCheck.FileName.Contains(">"))
            {
                valid = false;
                this.AddPostErrorMessage.Text = NSW.Data.LabelText.Text("AddPost.FileNameMessage");
            }
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.IsFileValid", "Done, returning : " + valid.ToString(), LogEnum.Debug);

            return valid;
        }

        /// <summary>
        /// checks if input is a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsNumber(string input)
        {
            try
            {
                Convert.ToInt32(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// takes input image and shrinks it to 250x200 px, then saves to a new location for later use
        /// </summary>
        /// <param name="original_image"></param>
        /// <param name="tmpName"></param>
        protected void OptimizeNResize(Bitmap original_image, string tmpName)
        {
            try
            {
                Bitmap final_image = null;
                Graphics graphic = null;
                int reqW = 250;
                int reqH = 200;
                final_image = new Bitmap(reqW, reqH);
                graphic = Graphics.FromImage(final_image);
                graphic.FillRectangle(new SolidBrush(Color.Transparent),
                    new Rectangle(0, 0, reqW, reqH));
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic; /* new way */
                graphic.DrawImage(original_image, 0, 0, reqW, reqH);
                // save the mobile version
                final_image.Save(tmpName);
                // dispose of memory objects
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (final_image != null) final_image.Dispose();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "AddPost.OptimizeNResize", x, LogEnum.Critical);
            }
        }

    }
}