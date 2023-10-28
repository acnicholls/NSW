using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;


namespace NSW.Repositories
{
	public class PostRepository : BaseRepository, IPostRepository
    {

		private readonly IRepository<User> _userRepository;
		private readonly ILabelTextRepository _labelTextRepository;

		public PostRepository(
			ILog log,
			IUser user,
			IProjectInfo projectInfo,
			IRepository<User> userRepo,
			ILabelTextRepository labelTextRepository
			) : base(log, user, projectInfo) 
		{
			_userRepository = userRepo;
			_labelTextRepository = labelTextRepository;
		}

		public Post? GetByIdentifier(string identifier)
		{
			throw new NotImplementedException();
		}

		public IList<Post> GetAll()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// builds a post based on the integer ID
		/// </summary>
		/// <param name="id">ID of the post</param>
		public Post? GetById(int id)
        {
			Post post = new Post();
            try
            {
				DataSet ds = base.GetDataFromSqlString("Select * from tblPosts where fldPost_id=" + id.ToString());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
					post.ID = Convert.ToInt32(dr["fldPost_id"]);
					post.CategoryID = Convert.ToInt32(dr["fldPost_CategoryID"]);
					post.Title = dr["fldPost_Title"].ToString();
					post.Description = dr["fldPost_Description"].ToString();
					post.Price = Convert.ToDecimal(dr["fldPost_Price"]);
					post.Expiry = Convert.ToDateTime(dr["fldPost_Expiry"]);
					post.UserID = Convert.ToInt32(dr["fldPost_UserID"]);
					post.Status = dr["fldPost_Status"].ToString();
					post.DeleteFlag = Convert.ToBoolean(dr["fldPost_DeleteFlag"]);
                }
                else
                {
                    Exception ex = new Exception("There are either no rows, or too many rows with the same ID " + id.ToString());
                    throw ex;
                }
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.ByID", x, LogEnum.Critical);
            }
			return post;
        }

        /// <summary>
        /// saves a new post to the database
        /// </summary>
        public Post Insert(Post post)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@catID", post.CategoryID);
                parameters.Add(param);
                param = new SqlParameter("@title", post.Title);
                parameters.Add(param);
                param = new SqlParameter("@desc", post.Description);
                parameters.Add(param);
                param = new SqlParameter("@price", post.Price);
                parameters.Add(param);
                param = new SqlParameter("@userID", post.UserID);
                parameters.Add(param);
                param = new SqlParameter("@ID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                parameters.Add(param);

				var result = base.ExecuteStoreProcedure("insertPost", parameters);
				post.ID = Convert.ToInt32(param.Value);
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.insertPost", "PostRepository inserted result : " + result.ToString(), LogEnum.Debug);
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.insertPost", x, LogEnum.Critical);
            }
			return post;
        }

        /// <summary>
        /// saves new values to the current post
        /// </summary>
        public Post Modify(Post post)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ID", post.ID);
                parameters.Add(param);
                param = new SqlParameter("@title", post.Title);
                parameters.Add(param);
                param = new SqlParameter("@desc", post.Description);
                parameters.Add(param);
                param = new SqlParameter("@price", post.Price);
                parameters.Add(param);
                param = new SqlParameter("@status", post.Status);
                parameters.Add(param);
				var result = base.ExecuteStoreProcedure("modifyPost", parameters);
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.insertPost", "PostRepository modified result : " + result.ToString(), LogEnum.Debug);
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.modifyPost", x, LogEnum.Critical);
            }
			return post;
        }

        /// <summary>
        /// deletes the current post
        /// </summary>
        public void Delete(Post post)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ID", post.ID);
                parameters.Add(param);
				var result = base.ExecuteStoreProcedure("deletePost", parameters);
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.deletePost", "PostRepository deleted result : " + result.ToString(), LogEnum.Debug);
			}
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.deletePost", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// gets the user object for the current post
        /// </summary>
        /// <returns>user object</returns>
        public IUser PostUser(Post post)
        {
            try
            {
                if (post.UserID != 0)
                {
                    return _userRepository.GetById(post.UserID);
                }
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.PostUser", x, LogEnum.Critical);
            }
			return new User();
        }

        /// <summary>
        /// sends an email to the current posts user indicating post expiry
        /// </summary>
        public void SendExpiryEmail(Post post)
        {
            try
            {
				var emailDetails = _labelTextRepository.GetListOfGroupedLabels("ExpiryEmail");

				NSW.Info.EmailMessage email = new Info.EmailMessage();
                IUser thisUser = PostUser(post);
                email.To.Add(thisUser.Email);
                email.Subject = emailDetails[".Subject"];
                string strBody = emailDetails[".Line1"] + " " + post.Title + "\r\n\r\n";
                strBody += "\r\n";
                strBody += emailDetails[".Line2"];
                strBody += "\r\n\r\n";
                string strLink = _projectInfo.protocol + _projectInfo.webServer + "/Posts/RenewPost.aspx?postID=" + post.ID.ToString();
                strBody += strLink;
                strBody += "\r\n\r\n";
                strBody += emailDetails[".Line3"] + "\r\n";
                strBody += emailDetails[".Line4"];
                email.Body = strBody;
                email.Send();
                SetEmailSent(post);
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.SendExpiryEmail", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// if email is successfully sent, sets flag in the database
        /// </summary>
        public void SetEmailSent(Post post)
        {
            try
            {
				var result = base.ExecuteNonQuery("update tblPosts set fldPost_emailSent=1 where fldPost_id=" + post.ID.ToString());
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.SetEmailSent", "PostRepository.SetEmailSent result: " + result.ToString(), LogEnum.Critical);
			}
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.SetEmailSent", x, LogEnum.Critical);
            }
        }


    }
}
