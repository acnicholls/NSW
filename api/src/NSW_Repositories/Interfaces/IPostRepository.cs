using NSW.Data;
using NSW.Data.Interfaces;

namespace NSW.Repositories.Interfaces
{
	public interface IPostRepository : IRepository<Post>
	{
		IUser PostUser(Post post);


		void SetEmailSent(Post post);

		IList<Post> GetByCategoryId(int categoryId);
		IList<Post> GetByUserId(int userId);
	}
}
