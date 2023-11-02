using NSW.Data;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;

namespace NSW.Services
{
	public class PostalCodeService : IService<PostalCode>
	{
		private readonly IRepository<PostalCode> _repository;

		public PostalCodeService(IRepository<PostalCode> repository)
		{
			_repository = repository;
		}

		public void Delete(PostalCode entity) => _repository.Delete(entity);

		public IList<PostalCode> GetAll() => _repository.GetAll();

		public PostalCode? GetById(int id) => _repository.GetById(id);

		public PostalCode? GetByIdentifier(string identifier) => _repository.GetByIdentifier(identifier);

		public PostalCode Insert(PostalCode entity) => _repository.Insert(entity);

		public PostalCode Modify(PostalCode entity) => _repository.Modify(entity);
	}
}
