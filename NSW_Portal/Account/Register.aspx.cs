﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace NSW.Account
{
    public partial class Register : PageBase
    {
        NSW.Data.User newUser;
        NSW.Data.Security.MembershipProvider Membership = new NSW.Data.Security.MembershipProvider();

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.Page_Load", "Here", LogEnum.Debug);
                // load all text values
                this.LoadLabelValues();
                // now for other logic
                if (!IsPostBack)
                {
                    // initialize the newUser object
                    newUser = new Data.User();
                    // add postal codes to the drop down
                    SqlConnection regConn = new SqlConnection(NSW.Info.ConnectionInfo.ConnectionString);
                    SqlCommand regComm = regConn.CreateCommand();
                    regComm.CommandType = CommandType.Text;
                    regComm.CommandText = "Select fldPostal_Code from tblPostalCodes";
                    DataSet ds = new DataSet();
                    SqlDataAdapter adap = new SqlDataAdapter(regComm);
                    regConn.Open();
                    adap.Fill(ds);
                    regConn.Close();
                    this.PostalCode.Items.Clear();
                    this.PostalCode.Items.Add("Select One...");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        this.PostalCode.Items.Add(dr["fldPostal_Code"].ToString());
                    }
                }
                else
                {
                    NSW.Data.User cacheUser = (NSW.Data.User)NSW.Data.Cache.Get("User");
                    if (cacheUser != null)
                        newUser = cacheUser;
                    else
                        newUser = new Data.User();
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.Page_Load", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// finalizes user creation, logs in new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.RegisterUser_CreatedUser", "Here", LogEnum.Debug);
                // now log the user in
                FormsAuthentication.SetAuthCookie(newUser.Email, false /* createPersistentCookie */);
                string continueUrl = Request.QueryString["ReturnUrl"];
                if (String.IsNullOrEmpty(continueUrl))
                {
                    continueUrl = "~/Default.aspx";
                }
                Response.Redirect(continueUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.RegisterUser_CreatedUser", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// send user back on wizard step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PreviousClick(object sender, WizardNavigationEventArgs e)  
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.PreviousClick", "Here", LogEnum.Debug);
            // go back one screen.
        }

        /// <summary>
        /// validates form data, and creates new user row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)  
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.FinishClick", "Here", LogEnum.Debug);
                string errorMessage = "";
                bool valid = true;
                // grab screen values
                TextBox phone = (TextBox)RegisterUser.ActiveStep.FindControl("PhoneNumber");
                DropDownList postal = (DropDownList)RegisterUser.ActiveStep.FindControl("PostalCode");
                // test screen values
                string phoneNumber = phone.Text;
                phoneNumber = phoneNumber.Replace(" ", string.Empty);
                if (phoneNumber.Length > 0)
                {
                    if (phoneNumber.Length == 10 | phoneNumber.Length == 11)
                    { }
                    else
                    {
                        errorMessage += NSW.Data.LabelText.Text("Register.ValidPhone") + "\r\n";
                        valid = false;
                    }
                }
                if (postal.SelectedItem.Text == "Select One...")
                {
                    errorMessage += NSW.Data.LabelText.Text("Register.ValidPostal") + "\r\n";
                    valid = false;
                }
                if (!EnglishPrefRadio.Checked & !JapanesePrefRadio.Checked)
                {
                    valid = false;
                    errorMessage += NSW.Data.LabelText.Text("Profile.LangRequired") + "\r\n";
                } 
                this.Captcha1.ValidateCaptcha(this.txtCaptcha.Text.Trim());
                if (!Captcha1.UserValidated)
                {
                    errorMessage += NSW.Data.LabelText.Text("Register.BadCaptcha");
                    valid = false;
                }
                if (valid)
                {
                    // add the values to the cahced user and save to db
                    newUser.Phone = phone.Text.Trim();
                    newUser.PostalCode = postal.SelectedItem.Text.Trim();
                    LanguagePreference lp = new LanguagePreference();
                    if (EnglishPrefRadio.Checked)
                        lp = LanguagePreference.English;
                    if (JapanesePrefRadio.Checked)
                        lp = LanguagePreference.Japanese;
                    newUser.LanguagePreference = (int)lp;
                    NSW.Data.Cache.Add("User", newUser);
                    newUser.insertUser();
                    RegisterUser_CreatedUser(sender, EventArgs.Empty);
                }
                else
                {
                    this.Step1ErrorMessage.Text = errorMessage;
                    e.Cancel = true;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.FinishClick", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// validates step data and creates new user object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NextClick(object sender, WizardNavigationEventArgs e)    
        {
            try
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.NextClick", "Here", LogEnum.Debug);
                string errorMessage = "";
                bool valid = true;
                // grab screen values
                TextBox username = (TextBox)RegisterUser.ActiveStep.FindControl("UserName");
                TextBox password = (TextBox)RegisterUser.ActiveStep.FindControl("Password");
                TextBox email = (TextBox)RegisterUser.ActiveStep.FindControl("Email");
                // test the values
                if (NSW.Data.User.Exists(email.Text.Trim()))
                {
                    errorMessage += NSW.Data.LabelText.Text("Register.EmailInUse");
                    valid = false;
                }
                if (password.Text.Length < Membership.MinRequiredPasswordLength)
                {
                    errorMessage += NSW.Data.LabelText.Text("Register.PageInstructions2.Part1").ToString() + " " + Membership.MinRequiredPasswordLength.ToString() + " " + NSW.Data.LabelText.Text("Register.PageInstructions2.Part2").ToString();
                    valid = false;
                }
                if (valid)
                {
                    // add the values to the user object and cache it
                    newUser.Name = username.Text.Trim();
                    newUser.Password = password.Text.Trim();
                    newUser.Email = email.Text.Trim();
                    NSW.Data.Cache.Add("User", newUser);
                }
                else
                {
                    this.Step0ErrorMessage.Text = errorMessage;
                    //this.Step0ErrorMessage.Visible = true;
                    e.Cancel = true;
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.NextClick", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            try
            {
                // declare all text values for all labels
                this.PageTitle.Text = NSW.Data.LabelText.Text("Register.Title");
                this.PageInstructions1.Text = NSW.Data.LabelText.Text("Register.PageInstructions1");
                // create 
                string message = NSW.Data.LabelText.Text("Register.PageInstructions2.Part1").ToString();
                message += " " + Membership.MinRequiredPasswordLength.ToString();
                message += " " + NSW.Data.LabelText.Text("Register.PageInstructions2.Part2").ToString();

                this.PageInstructions2.Text = message;
                this.PageInstructions3.Text = NSW.Data.LabelText.Text("Register.PageInstructions3");

                (this.RegisterUserWizardStep0.FindControl("UserNameLabel") as Label).Text = NSW.Data.LabelText.Text("Register.UserNameLabel");
                (this.RegisterUserWizardStep0.FindControl("UserNameRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Register.UserNameRequired");
                (this.RegisterUserWizardStep0.FindControl("EmailLabel") as Label).Text = NSW.Data.LabelText.Text("Register.EmailLabel");
                (this.RegisterUserWizardStep0.FindControl("EmailRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Register.EmailRequired");
                (this.RegisterUserWizardStep0.FindControl("IsEmail") as RegularExpressionValidator).ErrorMessage = NSW.Data.LabelText.Text("Register.IsEmail");
                (this.RegisterUserWizardStep0.FindControl("PasswordLabel") as Label).Text = NSW.Data.LabelText.Text("Register.PasswordLabel");
                (this.RegisterUserWizardStep0.FindControl("PasswordRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Register.PasswordRequired");
                (this.RegisterUserWizardStep0.FindControl("ConfirmPasswordLabel") as Label).Text = NSW.Data.LabelText.Text("Register.ConfirmPasswordLabel");
                (this.RegisterUserWizardStep0.FindControl("ConfirmPasswordRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Register.ConfirmPasswordRequired");
                (this.RegisterUserWizardStep0.FindControl("PasswordCompare") as CompareValidator).ErrorMessage = NSW.Data.LabelText.Text("Register.PasswordCompare");
                (this.RegisterUserWizardStep1.FindControl("PhoneLabel") as Label).Text = NSW.Data.LabelText.Text("Register.PhoneLabel");
                (this.RegisterUserWizardStep1.FindControl("PostalCodeLabel") as Label).Text = NSW.Data.LabelText.Text("Register.PostalCodeLabel");
                (this.RegisterUserWizardStep1.FindControl("LanguagePreferenceLabel") as Label).Text = NSW.Data.LabelText.Text("Register.LanguagePreferenceLabel");
                Data.LabelText jap = new Data.LabelText("Japanese");
                Data.LabelText eng = new Data.LabelText("English");
                (this.RegisterUserWizardStep1.FindControl("EnglishPrefRadio") as RadioButton).Text = eng.English + " " + eng.Japanese;
                (this.RegisterUserWizardStep1.FindControl("JapanesePrefRadio") as RadioButton).Text = jap.English + " " + jap.Japanese;
                (Global.GetControlFromWizard(this.RegisterUser, WizardNavigationTempContainer.StartNavigationTemplateContainerID, "StartNextButton") as Button).Text = NSW.Data.LabelText.Text("Register.StartNextButton");
                (Global.GetControlFromWizard(this.RegisterUser, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("Register.FinishButton");
                (Global.GetControlFromWizard(this.RegisterUser, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishPreviousButton") as Button).Text = NSW.Data.LabelText.Text("Register.PreviousButton");
            }
            catch (Exception x)
            {

                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.LoadLabelValues", x, LogEnum.Critical);
            }
        }
    }
}
