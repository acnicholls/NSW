using NSW.Data;
using NSW.Interfaces;
using NSW.Repositories.Interfaces;
using System.Data;

namespace NSW.Repositories
{
	public class PostalCodeRepository : BaseRepository, IRepository<PostalCode>
    {
		public PostalCodeRepository(IUser user): base(user) { }



		public IList<PostalCode> GetAll()
		{
			throw new NotImplementedException();
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
				Log.WriteToLog(NSW.Info.ProjectInfo.ProjectLogType, "PostalCode.ByCode", x, LogEnum.Critical);
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
