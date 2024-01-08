using NSW.Data;

namespace NSW.Repositories.Interfaces
{
    public interface IUserRepository 
	{
		User GetByEmail(string email);
		bool ExistsByEmail(string email);
        bool ExistsById(int id);

        IList<User> GetAll();
        User? GetById(int id);
        User Insert(User entity);
        User Modify(User entity);
        void Delete(User entity);
    }
}
