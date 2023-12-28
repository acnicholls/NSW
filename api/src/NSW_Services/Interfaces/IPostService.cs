using NSW.Data;

namespace NSW.Services.Interfaces
{
	public interface IPostService : IService<Post>
	{
		void SendExpiryEmail(Post post);

		IList<Post> GetByCategoryId(int categoryId);

		IList<Post> GetByUserId(int userId);

	}
}
