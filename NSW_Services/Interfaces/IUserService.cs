using NSW.Data;
using NSW.Interfaces;

namespace NSW.Services.Interfaces
{
	public interface IUserService :IService<User>
	{
		User GetByEmailAndPassword(string email, string password);
		User GetByEmail(string email);
		bool Exists(string email);
		void ChangePassword(IUser user, string newPassword);
	}
}
