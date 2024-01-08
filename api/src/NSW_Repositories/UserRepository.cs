using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info;
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
			IProjectInfo projectInfo,
			IConnectionInfo connectionInfo
			) : base(log, user, projectInfo, connectionInfo)
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
                user = ConvertDataRowToUser(dr);
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.ByID", x, LogEnum.Critical);
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
					user = ConvertDataRowToUser(dr);
                }
                else
					user.Id = 0;
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
        public bool ExistsByEmail(string email)	
        {
            bool returnValue = false;
            try
            {
				// check for username or email.
				DataSet ds = base.GetDataFromSqlString("Select * from tblUsers where fldUser_Email='" + email + "'");
                // if any row then the username or email already exists
                int result = ds.Tables[0].Rows.Count;
                if (result == 1)
                    returnValue = true;
            }
            catch   (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.ExistsByEmail", x, LogEnum.Critical);
            }
            return returnValue;
        }

        public bool ExistsById(int id)
        {
            bool returnValue = false;
            try
            {
                // check for username or email.
                DataSet ds = base.GetDataFromSqlString("Select * from tblUsers where fldUser_id=" + id.ToString());
                // if any row then the username or email already exists
                int result = ds.Tables[0].Rows.Count;
                if (result == 1)
                    returnValue = true;
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "User.ExistsById", x, LogEnum.Critical);
            }
            return returnValue;
        }


        public IList<User> GetAll()
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
                param = new SqlParameter("@id", entity.Id);
                parameters.Add(param);
                param = new SqlParameter("@username", entity.UserName);
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
                param = new SqlParameter("@id", entity.Id);
                parameters.Add(param);
                param = new SqlParameter("@username", entity.UserName);
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
				param = new SqlParameter("@ID", entity.Id);
				parameters.Add(param);
				// execute the command
				var result = base.ExecuteStoreProcedure("deleteUser", parameters);
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "User.deleteUser", x, LogEnum.Critical);
			}
		}

        private User ConvertDataRowToUser(DataRow dr)
        {
            var user = new User
            {
                Id = Convert.ToInt32(dr["fldUser_id"]),
                UserName = dr["fldUser_UserName"].ToString(),
                Email = dr["fldUser_Email"].ToString(),
                Phone = dr["fldUser_Phone"].ToString(),
                PostalCode = dr["fldUser_PostalCode"].ToString(),
                Role = dr["fldUser_Role"].ToString(),
                LanguagePreference = Convert.ToInt32(dr["fldUser_langPref"]),
            };
            return user;
        }
	}
}
