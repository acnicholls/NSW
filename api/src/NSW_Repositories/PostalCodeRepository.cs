using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info;
using NSW.Info.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;

namespace NSW.Repositories
{
	public class PostalCodeRepository : BaseRepository, IRepository<PostalCode>
	{
		public PostalCodeRepository(
			ILog log,
			IUser user,
			IProjectInfo projectInfo,
			IConnectionInfo connectionInfo
			) : base(log, user, projectInfo, connectionInfo) { }



		public IList<PostalCode> GetAll()
		{
			List<PostalCode> postalCodes = new List<PostalCode>();
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblPostalCodes");
				if (ds.Tables[0].Rows.Count > 1)
				{
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						var newCode = new PostalCode();
						newCode.Code = dr["fldPostal_Code"].ToString();
						newCode.Longitude = Convert.ToDecimal(dr["fldPostal_Longitude"]);
						newCode.Latitude = Convert.ToDecimal(dr["fldPostal_Latitude"]);
						postalCodes.Add(newCode);
					}
				}
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostalCode.ByCode", x, LogEnum.Critical);
			}
			return postalCodes;
		}

		public PostalCode? GetById(int id)
		{
			throw new NotImplementedException();
		}

		public PostalCode? GetByIdentifier(string identifier)
		{
			PostalCode postalCode = new PostalCode();
			try
			{
				DataSet ds = base.GetDataFromSqlString("Select * from tblPostalCodes where fldPostal_Code='" + identifier + "'");
				if (ds.Tables[0].Rows.Count == 1)
				{
					postalCode.Code = ds.Tables[0].Rows[0]["fldPostal_Code"].ToString();
					postalCode.Longitude = Convert.ToDecimal(ds.Tables[0].Rows[0]["fldPostal_Longitude"]);
					postalCode.Latitude = Convert.ToDecimal(ds.Tables[0].Rows[0]["fldPostal_Latitude"]);
				}
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostalCode.ByCode", x, LogEnum.Critical);
			}
			return postalCode;
		}

		public PostalCode Insert(PostalCode entity)
		{
			throw new NotImplementedException();
		}

		public PostalCode Modify(PostalCode entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(PostalCode entity)
		{
			throw new NotImplementedException();
		}
	}
}
