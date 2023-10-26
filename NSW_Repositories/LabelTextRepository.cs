using Microsoft.Data.SqlClient;
using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;
using NSW.Enums;



namespace NSW.Repositories
{
	public class LabelTextRepository : BaseRepository, ILabelTextRepository
    {


        public LabelTextRepository(IUser user) : base(user)
        {
               
        }

		public LabelText? GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IList<LabelText> GetAll()
		{
			List<LabelText> returnValue = new List<LabelText>();
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblLabelText");
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					returnValue.Add(new LabelText()
					{
						ID = dr["fldLabel_ID"].ToString(),
						English = dr["fldLabel_English"].ToString(),
						Japanese = dr["fldLabel_Japanese"].ToString()
					});
				}
			}
			catch (Exception x)
			{
				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.GetByIdentifier", x, LogEnum.Critical); 
			}
			return returnValue;
		}

		public LabelText GetByIdentifier(string identifier)
		{
			LabelText text = new LabelText();
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblLabelText where fldLabel_ID='" + identifier + "'");
				DataRow dr = ds.Tables[0].Rows[0];
				text.ID = dr["fldLabel_ID"].ToString();
				text.English = dr["fldLabel_English"].ToString();
				text.Japanese = dr["fldLabel_Japanese"].ToString();
			}
			catch (Exception x)
			{
				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.GetByIdentifier", x, LogEnum.Critical);
			}
			return text;
		}

        /// <summary>
        /// grabs a text string in required language
        /// </summary>
        /// <param name="identifier">ID key of labeltext row</param>
        /// <returns>text string in desired language</returns>
        public string GetTextByIdentifier(string identifier)
        {
            try
            {
				DataSet ds = base.GetDataFromSqlString("Select * from tblLabelText where fldLabel_ID='" + identifier + "'");
                DataRow dr = ds.Tables[0].Rows[0];
                switch (base._currentUser.DisplayLanguage)
                {
                    case Enums.LanguagePreferenceEnum.English:
                        {
                            return dr["fldLabel_English"]?.ToString();
                        }
                    case Enums.LanguagePreferenceEnum.Japanese:
                        {
                            return dr["fldLabel_Japanese"]?.ToString();
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.GetTextByIdentifier", x, LogEnum.Critical);
            }
            return string.Empty;
        }

		public IDictionary<string, string> GetListOfGroupedLabels(string groupIdentifier)
		{
			var returnValue = new Dictionary<string, string>();
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblLabelText where fldLabelText_Id like '" + groupIdentifier + "%';");
				DataTable dt = ds.Tables[0];
				foreach(DataRow row in dt.Rows)
				{
					var fullString = row["fldLabelText_ID"].ToString();
					string key= fullString.Remove(1, groupIdentifier.Length);
					string value = "";
					switch (_currentUser.DisplayLanguage)
					{
						case Enums.LanguagePreferenceEnum.English:
							{
								value = row["fldLabel_English"].ToString();
								break;
							}
						default:
							{
								value = row["fldLabel_Japanese"].ToString();
								break;
							}
					}
					returnValue.Add(key, value);
				}
			}
			catch (Exception x)
			{

				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.GetTextWithPreferenceByIdentifier", x, LogEnum.Critical);
			}
			return returnValue;
		}

        /// <summary>
        /// gets a text string in the preferred language of the input user
        /// </summary>
        /// <param name="identifier">string ID of the labeltext row</param>
        /// <returns>desired text string</returns>
        public string GetTextWithPreferenceByIdentifier(string identifier)
        {
            try
            {
				DataSet ds = base.GetDataFromSqlString("Select * from tblLabelText where fldLabel_ID='" + identifier + "'");
                DataRow dr = ds.Tables[0].Rows[0];
                switch (_currentUser.DisplayLanguage)
                {
                    case Enums.LanguagePreferenceEnum.English:
                        {
                            return dr["fldLabel_English"].ToString();
                        }
                    case Enums.LanguagePreferenceEnum.Japanese:
                        {
                            return dr["fldLabel_Japanese"].ToString();
                        }
                }
            }
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.GetTextWithPreferenceByIdentifier", x, LogEnum.Critical);
            }
            return string.Empty;
        }

        /// <summary>
        /// saves new values to labeltext data row
        /// </summary>
        public LabelText Modify(LabelText label)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@id", label.ID);
                parameters.Add(param);
                param = new SqlParameter("@english", label.English);
                parameters.Add(param);
                param = new SqlParameter("@japanese", label.Japanese);
                parameters.Add(param);
                // execute the command
				var result = base.ExecuteStoreProcedure("modifyLabelText", parameters);
				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.Modify", "modifyLabelText result: " + result.ToString(), LogEnum.Debug);
			}
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.Modify", x, LogEnum.Critical);
            }
			return label;
        }

        /// <summary>
        /// deletes the desired labeltext row
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(LabelText label)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@id", label.ID);
                parameters.Add(param);
                // execute the command
				var result = base.ExecuteStoreProcedure("deleteLabelText", parameters);
				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.Delete", "deleteLabelText result: " + result.ToString(), LogEnum.Debug);
			}
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.Delete", x, LogEnum.Critical);
            }
        }

        /// <summary>
        /// inserts a new labeltext row
        /// </summary>
        public LabelText Insert(LabelText label)
        {
            try
            {
				var parameters = new List<SqlParameter>();
                // set all the parameters
                SqlParameter param = new SqlParameter();
                // assign values
                param = new SqlParameter("@id", label.ID);
                parameters.Add(param);
                param = new SqlParameter("@english", label.English);
                parameters.Add(param);
                param = new SqlParameter("@japanese", label.Japanese);
                parameters.Add(param);
                // execute the command
				var result = base.ExecuteStoreProcedure("insertLabelText", parameters);
				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.Delete", "insertLabelText result: " + result.ToString(), LogEnum.Debug);
			}
            catch (Exception x)
            {
                Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "LabelTextRepository.Delete", x, LogEnum.Critical);
            }
			return label;
        }
    }
}
