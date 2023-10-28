using NSW.Data;

namespace NSW.Services.Interfaces
{
	public interface IPostService
	{
		void SendExpiryEmail(Post post);

	}
}
