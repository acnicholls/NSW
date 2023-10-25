using NSW.Data;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;


namespace NSW.Services
{
	public class PostService : IService<Post>
    {
		private readonly IRepository<Post> _repository;

		public PostService(IRepository<Post> repository)
		{
			_repository = repository;
		}

		public void Delete(Post entity) => _repository.Delete(entity);

		public IList<Post> GetAll() => _repository.GetAll();

		public Post? GetById(int id) => _repository.GetById(id);

		public Post? GetByIdentifier(string identifier) => _repository.GetByIdentifier(identifier);

		public Post Insert(Post entity) => _repository.Insert(entity);

		public Post Modify(Post entity) => _repository.Modify(entity);
	}
}
