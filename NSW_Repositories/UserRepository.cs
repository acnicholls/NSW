using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;

namespace NSW.Repositories
{

	public class UserRepository: BaseRepository, IUserRepository
	{
        public UserRepository(
			ILog log,
			IUser user,
			IProjectInfo projectInfo
			) : base(log, user, projectInfo)
		{
		}

		/// <summary>
		/// builds a user object based on the ID of the user row
		/// </summary>
		/// <param name="id">integer ID of the user row</param>
		public User GetById(int id)
        {
			var user = new User();
            try
            {
				DataSet ds = base.GetDataFromSqlString("Select * from tblUsers where fldUser_id=" + id.ToString());
                // first find the user row in the database
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
				user.ID = id;
				user.Name = dr["fldUser_Name"].ToString();
				user.Password = dr["fldUser_Password"].ToString();
				user.Phone = dr["fldUser_Phone"].ToString();
				user.PostalCode = dr["fldUser_PostalCode"].ToString();
				user.Email = dr["fldUser_Email"].ToString();
				user.Role = dr["fldUser_Role"].ToString();
				user.LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]);
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.ByID", x, LogEnum.Critical);
            }
			return user;
        }

        /// <summary>
        /// builds a user object based on the email and password of the desired user
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="password">password of user</param>
        public User GetByEmailAndPassword(string email, string password)
        {
			var user = new User();
            try
            {
				DataSet ds = base.GetDataFromSqlString("Select * from tblUsers where fldUser_Email='" + email + "' and fldUser_Password='" + password + "'");
                // first find the user row in the database
                if (ds.Tables[0].Rows.Count == 1)
                {
                    // assign values
                    DataRow dr = ds.Tables[0].Rows[0];
					user.ID = Convert.ToInt32(dr["fldUser_id"]);
					user.Name = dr["fldUser_Name"].ToString();
					user.Password = dr["fldUser_Password"].ToString();
					user.Phone = dr["fldUser_Phone"].ToString();
					user.PostalCode = dr["fldUser_PostalCode"].ToString();
					user.Email = dr["fldUser_Email"].ToString();
					user.Role = dr["fldUser_Role"].ToString();
					user.LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]);
                }
                else
					user.ID = 0;
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.ByEmailAndPassword", x, LogEnum.Critical);
            }
			return user;
        }

        /// <summary>
        /// builds user object based solely on user email
        /// </summary>
        /// <param name="email">email of desired user</param>
        public User GetByEmail(string email)
        {
			var user = new User();
            try
            {
				DataSet ds = base.GetDataFromSqlString("Select * from tblUsers where fldUser_Email='" + email + "'");
                // first find the user row in the database
                if (ds.Tables[0].Rows.Count == 1)
                {
                    // assign values
                    DataRow dr = ds.Tables[0].Rows[0];
					user.ID = Convert.ToInt32(dr["fldUser_id"]);
					user.Name = dr["fldUser_Name"].ToString();
					user.Password = dr["fldUser_Password"].ToString();
					user.Phone = dr["fldUser_Phone"].ToString();
					user.PostalCode = dr["fldUser_PostalCode"].ToString();
					user.Email = dr["fldUser_Email"].ToString();
					user.Role = dr["fldUser_Role"].ToString();
					user.LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]);
                }
                else
					user.ID = 0;
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.ByEmail", x, LogEnum.Critical);
            }
			return user;
        }

        /// <summary>
        /// checks to see if the input email already exists in the current dataset
        /// </summary>
        /// <param name="email">email to check for</param>
        /// <returns>true if found, false if not</returns>
        public bool Exists(string email)	
        {
            try
            {
				// check for username or email.
				DataSet ds = base.GetDataFromSqlString("Select * from tblUsers where fldUser_Email='" + email + "'");
                // if any row then the username or email already exists
                int result = ds.Tables[0].Rows.Count;
                if (result > 0)
                    return true;
                else if (result == 0)
                    return false;
            }
            catch   (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.Exists", x, LogEnum.Critical);
            }
            return false;
        }

        /// <summary>
        /// changes user password to new value
        /// </summary>
        /// <param name="newPassword">new password to use</param>
        public void ChangePassword(IUser user, string newPassword)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@ID", user.ID);
                parameters.Add(param);
                param = new SqlParameter("@newPass", newPassword);
                parameters.Add(param);
                // execute the command
				var result = base.ExecuteStoreProcedure("modifyUserPassword", parameters);
				_log.WriteToLog(_projectInfo.ProjectLogType, "UserRepository.ChangePassword", "UserRepository modifyUserPassword result : " + result.ToString(), LogEnum.Debug);
			}
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "UserRepository.changePassword", x, LogEnum.Critical);
            }
        }

		public IList<User> GetAll()
		{
			throw new NotImplementedException();
		}

		public User? GetByIdentifier(string identifier)
		{
			throw new NotImplementedException();
		}

		public User Insert(User entity)
		{
			try
			{
				var parameters = new List<SqlParameter>();
				// set all the parameters
				SqlParameter param = new SqlParameter();
				// assign values
				param = new SqlParameter("@name", entity.Name);
				parameters.Add(param);
				param = new SqlParameter("@pass", entity.Password);
				parameters.Add(param);
				param = new SqlParameter("@email", entity.Email);
				parameters.Add(param);
				param = new SqlParameter("@phone", entity.Phone);
				parameters.Add(param);
				param = new SqlParameter("@postalcode", entity.PostalCode);
				parameters.Add(param);
				param = new SqlParameter("@langPref", entity.LanguagePreference);
				parameters.Add(param);
				// execute the command
				var result = base.ExecuteStoreProcedure("insertUser", parameters);
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "User.insertUser", x, LogEnum.Critical);
			}
			return entity;
		}

		public User Modify(User entity)
		{
			try
			{
				var parameters = new List<SqlParameter>();
				// set all the parameters
				SqlParameter param = new SqlParameter();
				// assign values
				param = new SqlParameter("@ID", entity.ID);
				parameters.Add(param);
				param = new SqlParameter("@name", entity.Name);
				parameters.Add(param);
				param = new SqlParameter("@pass", entity.Password);
				parameters.Add(param);
				param = new SqlParameter("@email", entity.Email);
				parameters.Add(param);
				param = new SqlParameter("@phone", entity.Phone);
				parameters.Add(param);
				param = new SqlParameter("@postalcode", entity.PostalCode);
				parameters.Add(param);
				param = new SqlParameter("@langPref", entity.LanguagePreference);
				parameters.Add(param);
				// execute the command
				var result = base.ExecuteStoreProcedure("modifyUser", parameters);
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "User.modifyUser", x, LogEnum.Critical);
			}
			return entity;
		}

		public void Delete(User entity)
		{
			try
			{
				var parameters = new List<SqlParameter>();
				// set all the parameters
				SqlParameter param = new SqlParameter();
				// assign values
				param = new SqlParameter("@ID", entity.ID);
				parameters.Add(param);
				// execute the command
				var result = base.ExecuteStoreProcedure("deleteUser", parameters);
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "User.deleteUser", x, LogEnum.Critical);
			}
		}
	}
}
