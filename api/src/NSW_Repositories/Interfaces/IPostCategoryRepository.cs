using NSW.Data;
using NSW.Data.DTO.Response;

namespace NSW.Repositories.Interfaces
{
	public interface IPostCategoryRepository : IRepository<PostCategory>
	{
		string GetTitleById(int ID);
		string GetDescriptionById(int ID);
		IList<PostCategoryPillResponse> GetPillList();

	}
}
