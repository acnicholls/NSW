using NSW.Data;
using NSW.Data.DTO.Response;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;

namespace NSW.Services
{
	public class PostCategoryService : IPostCategoryService
	{
		private readonly IPostCategoryRepository _repository;

		public PostCategoryService(IPostCategoryRepository repository)
		{
			_repository = repository;
		}

		public void Delete(PostCategory entity) => _repository.Delete(entity);

		public IList<PostCategory> GetAll() => _repository.GetAll();

		public PostCategory? GetById(int id) => _repository.GetById(id);

		public PostCategory? GetByIdentifier(string identifier) => _repository.GetByIdentifier(identifier);

		public PostCategory Insert(PostCategory entity) => _repository.Insert(entity);

		public PostCategory Modify(PostCategory entity) => _repository.Modify(entity);

		public IList<PostCategoryPillResponse> GetPillList() => _repository.GetPillList();
	}
}
