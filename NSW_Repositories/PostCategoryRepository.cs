using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW;
using NSW.Data.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;
using NSW.Info.Interfaces;

namespace NSW.Repositories
{
	public class PostCategoryRepository : BaseRepository, IPostCategoryRepository
    {



        public PostCategoryRepository(
			ILog log,
			IUser user,
			IProjectInfo projectInfo
			) : base(log, user, projectInfo)
		{
        }

        /// <summary>
        /// builds a post category object based on integer ID
        /// </summary>
        /// <param name="ID">integer ID of desired post category</param>
        public PostCategory? GetById(int ID)
        {
			PostCategory category = new PostCategory();
            try
            {
                DataSet ds = base.GetDataFromSqlString("Select * from tblPostCategories where fldPostCategory_id=" + ID.ToString());
                // now fill the values
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
					category.ID = Convert.ToInt32(dr["fldPostCategory_id"]);
					category.EnglishTitle = dr["fldPostCategory_English"].ToString();
					category.JapaneseTitle = dr["fldPostCategory_Japanese"].ToString();
					category.EnglishDescription = dr["fldPostCategory_DescEnglish"].ToString();
					category.JapaneseDescription = dr["fldPostCategory_DescJapanese"].ToString();
                }
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostCategory.GetById", x, LogEnum.Critical);
            }
			return category;
        }

        /// <summary>
        /// gets the text string title of the category in the desired language
        /// </summary>
        /// <param name="ID">ID of the category</param>
        /// <returns>string of text title</returns>
        public  string GetTitleById(int ID)
        {
			string returnValue = string.Empty;
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblPostCategories where fldPostCategory_id=" + ID);
                // assign values
                DataRow dr = ds.Tables[0].Rows[0];
				returnValue = GetLabelTextFromDataRow(dr);
            }
            catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostCategory.GetTitleById", x, LogEnum.Critical);
            }
            return returnValue;
        }

        /// <summary>
        /// gets the string describing the content of the category in the desired language
        /// </summary>
        /// <param name="ID">integer ID of category</param>
        /// <returns>string of description text</returns>
        public string GetDescriptionById(int ID) 
        {
			string returnValue = string.Empty;
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblPostCategories where fldPostCategory_id=" + ID);
                DataRow dr = ds.Tables[0].Rows[0];
				returnValue = GetLabelTextFromDataRow(dr);
			}
			catch (Exception x)
            {
                _log.WriteToLog(_projectInfo.ProjectLogType, "PostCategory.GetDescriptionById", x, LogEnum.Critical);
            }
			return returnValue;
		}

		public IList<PostCategory> GetAll()
		{
			throw new NotImplementedException();
		}

		public PostCategory? GetByIdentifier(string identifier)
		{
			throw new NotImplementedException();
		}

		public PostCategory Insert(PostCategory entity)
		{
			try
			{
				var parameters = new List<SqlParameter>();
				SqlParameter param = new SqlParameter();
				if (entity.EnglishTitle.Length > 0)
					param = new SqlParameter("@english", entity.EnglishTitle);
				else
					param = new SqlParameter("@english", string.Empty);
				parameters.Add(param);
				if (entity.JapaneseTitle.Length > 0)
					param = new SqlParameter("@japanese", entity.JapaneseTitle);
				else
					param = new SqlParameter("@japanese", string.Empty);
				parameters.Add(param);
				if (entity.EnglishDescription.Length > 0)
					param = new SqlParameter("@descEnglish", entity.EnglishDescription);
				else
					param = new SqlParameter("@descEnglish", string.Empty);
				parameters.Add(param);
				if (entity.JapaneseDescription.Length > 0)
					param = new SqlParameter("@descJapanese", entity.JapaneseDescription);
				else
					param = new SqlParameter("@descJapanese", string.Empty);
				parameters.Add(param);
				// now execute the procedure
				base.ExecuteStoreProcedure("insertPostCategory", parameters);
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostCategory.Insert", x, LogEnum.Critical);
			}
			return entity;
		}

		public PostCategory Modify(PostCategory entity)
		{
			try
			{
				var parameters = new List<SqlParameter>();
				SqlParameter param = new SqlParameter("@id", entity.ID);
				parameters.Add(param);
				if (entity.EnglishTitle.Length > 0)
					param = new SqlParameter("@english", entity.EnglishTitle);
				else
					param = new SqlParameter("@english", string.Empty);
				parameters.Add(param);
				if (entity.JapaneseTitle.Length > 0)
					param = new SqlParameter("@japanese", entity.JapaneseTitle);
				else
					param = new SqlParameter("@japanese", string.Empty);
				parameters.Add(param);
				if (entity.EnglishDescription.Length > 0)
					param = new SqlParameter("@descEnglish", entity.EnglishDescription);
				else
					param = new SqlParameter("@descEnglish", string.Empty);
				parameters.Add(param);
				if (entity.JapaneseDescription.Length > 0)
					param = new SqlParameter("@descJapanese", entity.JapaneseDescription);
				else
					param = new SqlParameter("@descJapanese", string.Empty);
				parameters.Add(param);
				// now execute the procedure
				base.ExecuteStoreProcedure("modifyPostCategory", parameters);
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostCategory.Modify", x, LogEnum.Critical);
			}
			return entity;
		}

		public void Delete(PostCategory entity)
		{
			throw new NotImplementedException();
		}
	}
}
