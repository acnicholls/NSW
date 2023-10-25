namespace NSW.Repositories.Interfaces
{
	public interface IRepository<T>
	{
		IList<T> GetAll();
		T? GetById(int id);
		T? GetByIdentifier(string identifier);
		T Insert(T entity);
		T Modify(T entity);
		void Delete(T entity);

	}
}
