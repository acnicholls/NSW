using NSW.Data;
using NSW.Interfaces;

namespace NSW.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		User GetByEmailAndPassword(string email, string password);
		User GetByEmail(string email);
		bool Exists(string email);
		void ChangePassword(IUser user, string newPassword);
	}
}
