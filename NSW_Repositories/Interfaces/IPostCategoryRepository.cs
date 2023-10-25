using NSW.Data;
using NSW.Repositories.Interfaces;

namespace NSW.Repositories.Interfaces
{
	public interface IPostCategoryRepository : IRepository<PostCategory>
	{
		string GetTitleById(int ID);
		string GetDescriptionById(int ID);

	}
}
