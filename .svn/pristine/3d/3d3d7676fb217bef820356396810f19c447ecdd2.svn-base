using System;

namespace NSW.Data.Security
{
    public class MembershipProvider : System.Web.Security.SqlMembershipProvider
    {

        public override bool RequiresUniqueEmail
        {
            get
            {
                return base.RequiresUniqueEmail;
            }
        }

        public override string ApplicationName
        {
            get
            {
                return base.ApplicationName;
            }
            set
            {
                base.ApplicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return base.EnablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return base.EnablePasswordRetrieval;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return base.MaxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return base.MinRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return 6;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return base.PasswordAttemptWindow;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return base.PasswordStrengthRegularExpression;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return base.RequiresQuestionAndAnswer;
            }
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return base.ChangePassword(username, oldPassword, newPassword);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return true;
        }

        public NSW.Data.User CreateUser(string username, string password, string email, string postalCode, string phone)
        {
            
            User newUser = new User();
            newUser.Email = email;
            newUser.Name = username;
            newUser.Password = password;
            newUser.PostalCode = postalCode;
            newUser.Phone = phone;
            newUser.insertUser();
            return newUser;
        }

        public override bool ValidateUser(string username, string password)
        {
            User loginUser = new User(username, password);
            if (loginUser.ID == 0)
                return false;
            else
            {
                return true;
            }
        }
   }
}
