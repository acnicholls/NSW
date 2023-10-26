using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;

namespace NSW.Services
{

	public class UserService : IUserService
	{


		private readonly IUserRepository _repository;

		public UserService(IUserRepository repository)
		{
			_repository = repository;
		}

		public void ChangePassword(IUser user, string newPassword) => _repository.ChangePassword(user, newPassword);

		public void Delete(User entity) => _repository.Delete(entity);

		public bool Exists(string email) => _repository.Exists(email);

		public IList<User> GetAll() => _repository.GetAll();

		public User GetByEmail(string email) => _repository.GetByEmail(email);
		public User GetByEmailAndPassword(string email, string password) => _repository.GetByEmailAndPassword(email, password);

		public User? GetById(int id) => _repository.GetById(id);

		public User? GetByIdentifier(string identifier) => _repository.GetByIdentifier(identifier);

		public User Insert(User entity) => _repository.Insert(entity);

		public User Modify(User entity) => _repository.Modify(entity);

	}
}
