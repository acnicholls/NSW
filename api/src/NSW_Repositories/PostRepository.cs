using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info;
using NSW.Info.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;
using System.Net.Mail;

namespace NSW.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {

        private readonly IUserRepository _userRepository;
        private readonly ILabelTextRepository _labelTextRepository;
        private readonly IServiceProvider _serviceProvider;


        public PostRepository(
            ILog log,
            IUser user,
            IProjectInfo projectInfo,
            IConnectionInfo connectionInfo,
            IUserRepository userRepo,
            ILabelTextRepository labelTextRepository,
            IServiceProvider serviceProvider
            ) : base(log, user, projectInfo, connectionInfo)
        {
            _userRepository = userRepo;
            _labelTextRepository = labelTextRepository;
            _serviceProvider = serviceProvider;
        }

        public Post? GetByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public IList<Post> GetAll()
        {
            List<Post> posts = new List<Post>();
            try
            {
                DataSet ds = base.GetDataFromSqlString("Select * from tblPosts");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        posts.Add(AssignFromDataRow(dr));
                    }
                }
                else
                {
                    Exception ex = new Exception("Something went wrong listing all posts.");
                    throw ex;
                }
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.GetAll", x, LogEnum.Critical);
            }
            return posts;
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
                    post = AssignFromDataRow(dr);
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

        private Post AssignFromDataRow(DataRow dr)
        {
            var post = new Post();
            post.ID = Convert.ToInt32(dr["fldPost_id"]);
            post.CategoryID = Convert.ToInt32(dr["fldPost_CategoryID"]);
            post.Title = dr["fldPost_Title"].ToString();
            post.Description = dr["fldPost_Description"].ToString();
            post.Price = Convert.ToDecimal(dr["fldPost_Price"]);
            post.Expiry = Convert.ToDateTime(dr["fldPost_Expiry"]);
            post.UserID = Convert.ToInt32(dr["fldPost_UserID"]);
            post.Status = dr["fldPost_Status"].ToString();
            post.DeleteFlag = Convert.ToBoolean(dr["fldPost_DeleteFlag"]);
            return post;
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

        public IList<Post> GetByCategoryId(int categoryId)
        {
            List<Post> posts = new List<Post>();
            try
            {
                DataSet ds = base.GetDataFromSqlString("Select * from tblPosts where fldPost_CategoryID=" + categoryId.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        posts.Add(AssignFromDataRow(dr));
                    }
                }
                else
                {
                    Exception ex = new Exception("Something went wrong listing all posts for category with id =" + categoryId.ToString());
                    throw ex;
                }
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.GetByCategoryId", x, LogEnum.Critical);
            }
            return posts;
        }

        public IList<Post> GetByUserId(int userId)
        {
            List<Post> posts = new List<Post>();
            try
            {
                DataSet ds = base.GetDataFromSqlString("Select * from tblPosts where fldPost_UserID=" + userId.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        posts.Add(AssignFromDataRow(dr));
                    }
                }
                else
                {
                    Exception ex = new Exception("Something went wrong listing all posts for user with id =" + userId.ToString());
                    throw ex;
                }
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.GetByUserId", x, LogEnum.Critical);
            }
            return posts;
        }
    }
}
