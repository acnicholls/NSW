using NSW.Data;
using NSW.Data.Interfaces;

namespace NSW.Repositories.Interfaces
{
	public interface IPostRepository : IRepository<Post>
	{
		IUser PostUser(Post post);

		void SendExpiryEmail(Post post);

		void SetEmailSent(Post post);
	}
}
