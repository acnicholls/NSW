using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSW.Account
{
    public partial class Profile : PageBase
    {
        NSW.Data.User newUser;
        NSW.Data.Security.MembershipProvider Membership = new Data.Security.MembershipProvider();

        /// <summary>
        /// loads page data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Register.Page_Load", "Here", LogEnum.Debug);
            // load all text values
            this.LoadLabelValues();
            // now for other logic
            // first get the user's current profile
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
                this.PostalCode.Items.Add(NSW.Data.LabelText.Text("SelectOne"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.PostalCode.Items.Add(dr["fldPostal_Code"].ToString());
                }
                // postalcode is loaded, now set user's values
                newUser = new Data.User(Page.User.Identity.Name.ToString());
                (this.ProfileWizardStep0.FindControl("UserName") as TextBox).Text = newUser.Name;
                (this.ProfileWizardStep0.FindControl("Email") as TextBox).Text = newUser.Email;
                if(newUser.Phone != null)
                    (this.ProfileWizardStep0.FindControl("PhoneNumber") as TextBox).Text = newUser.Phone;
                DropDownList ddl = (DropDownList)this.ProfileWizardStep0.FindControl("PostalCode");
                ddl.SelectedItem.Text = newUser.PostalCode;
                LanguagePreference langPref = (LanguagePreference)newUser.LanguagePreference;
                if(langPref == LanguagePreference.English)
                {
                    RadioButton rb = (RadioButton)this.ProfileWizardStep0.FindControl("EnglishPrefRadio");
                    RadioButton rb1 = (RadioButton)this.ProfileWizardStep0.FindControl("JapanesePrefRadio");
                    rb.Checked = true;
                    rb1.Checked = false;
                }
                if(langPref == LanguagePreference.Japanese)
                {
                    RadioButton rb = (RadioButton)this.ProfileWizardStep0.FindControl("JapanesePrefRadio");
                    RadioButton rb1 = (RadioButton)this.ProfileWizardStep0.FindControl("EnglishPrefRadio");
                    rb.Checked = true;
                    rb1.Checked = false;
                }
            }
        }

        /// <summary>
        /// sends the user back a wizard step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PreviousClick(object sender, WizardNavigationEventArgs e)  
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Profile.PreviousClick", "Here", LogEnum.Debug);
            // go back one screen.
        }

        /// <summary>
        /// validates form data and completes the wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishClick(object sender, WizardNavigationEventArgs e)  
        {
            Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "Profile.NextClick", "Here", LogEnum.Debug);
            string errorMessage = "";
            bool valid = true;
            // grab screen values
            TextBox username = (TextBox)ProfileWizard.ActiveStep.FindControl("UserName");
            TextBox email = (TextBox)ProfileWizard.ActiveStep.FindControl("Email");
            TextBox phone = (TextBox)ProfileWizard.ActiveStep.FindControl("PhoneNumber");
            DropDownList postal = (DropDownList)ProfileWizard.ActiveStep.FindControl("PostalCode");
            // test the values
             if (NSW.Data.User.Exists(email.Text.Trim()))
            {
                errorMessage += NSW.Data.LabelText.Text("Profile.EmailUsed");
                valid = false;
            }
            // test screen values
            string phoneNumber = phone.Text;
            phoneNumber = phoneNumber.Replace(" ", string.Empty);
            if (phoneNumber.Length > 0)
            {
                if (phoneNumber.Length == 10 | phoneNumber.Length == 11)
                { }
                else
                {
                    errorMessage = NSW.Data.LabelText.Text("Profile.ValidPhone");
                    valid = false;
                }
            }
            if (!EnglishPrefRadio.Checked & !JapanesePrefRadio.Checked)
            {
                valid = false;
                errorMessage += NSW.Data.LabelText.Text("Profile.LangRequired");
            }
            if (valid)
            {
                newUser = new Data.User(Page.User.Identity.Name.ToString());
                // add the values to the user object and cache it
                newUser.Name = username.Text.Trim();
                newUser.Email = email.Text.Trim();
                newUser.Phone = phoneNumber;
                newUser.PostalCode = postal.SelectedItem.Text.Trim();
                LanguagePreference lp = new LanguagePreference();
                if(EnglishPrefRadio.Checked)
                    lp = LanguagePreference.English;
                if(JapanesePrefRadio.Checked)
                    lp = LanguagePreference.Japanese;
                newUser.LanguagePreference = (int)lp;
                newUser.modifyUser();
                this.ErrorMessage.Text = NSW.Data.LabelText.Text("Profile.Success");
            }
            else
            {
                this.ErrorMessage.Text = errorMessage;
                e.Cancel = true;
            }
        }

        /// <summary>
        /// loads page text
        /// </summary>
        private void LoadLabelValues()
        {
            this.ProfileTitle.Text = NSW.Data.LabelText.Text("Profile.ProfileTitle");
            this.ProfileInstructions.Text = NSW.Data.LabelText.Text("Profile.ProfileInstructions");
            (this.ProfileWizardStep0.FindControl("UserNameLabel") as Label).Text = NSW.Data.LabelText.Text("Profile.UserNameLabel");
            (this.ProfileWizardStep0.FindControl("UserNameRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Profile.UserNameRequired");
            (this.ProfileWizardStep0.FindControl("EmailLabel") as Label).Text = NSW.Data.LabelText.Text("Profile.EmailLabel");
            (this.ProfileWizardStep0.FindControl("EmailRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Profile.EmailRequired");
            (this.ProfileWizardStep0.FindControl("IsEmail") as RegularExpressionValidator).ErrorMessage = NSW.Data.LabelText.Text("Profile.IsEmail");
            (this.ProfileWizardStep0.FindControl("PhoneLabel") as Label).Text = NSW.Data.LabelText.Text("Profile.PhoneLabel");
            (this.ProfileWizardStep0.FindControl("PostalCodeLabel") as Label).Text = NSW.Data.LabelText.Text("Profile.PostalCodeLabel");
            (this.ProfileWizardStep0.FindControl("PostalCodeRequired") as RequiredFieldValidator).ErrorMessage = NSW.Data.LabelText.Text("Profile.PostalCodeRequired");
            (this.ProfileWizardStep0.FindControl("LanguagePreferenceLabel") as Label).Text = NSW.Data.LabelText.Text("Profile.LanguagePreferenceLabel");
            Data.LabelText jap = new Data.LabelText("Japanese");
            Data.LabelText eng = new Data.LabelText("English");
            (this.ProfileWizardStep0.FindControl("EnglishPrefRadio") as RadioButton).Text = eng.English + " " + eng.Japanese;
            (this.ProfileWizardStep0.FindControl("JapanesePrefRadio") as RadioButton).Text = jap.English + " " + jap.Japanese;
            (Global.GetControlFromWizard(this.ProfileWizard, WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "FinishButton") as Button).Text = NSW.Data.LabelText.Text("Profile.FinishButton");
        }
    }
}
