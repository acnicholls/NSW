using Microsoft.AspNetCore.Mvc;

namespace NSW.Api.Controllers.Interfaces
{
    public interface IController<T>
    {
        Task<ActionResult<IList<T>>> GetAllAsync();
        Task<ActionResult<T?>> GetByIdAsync(int id);
        Task<ActionResult<T?>> GetByIdentifierAsync(string identifier);
        Task<ActionResult<T>> InsertAsync(T entity);
        Task<ActionResult<T>> ModifyAsync(T entity);
        Task<ActionResult> DeleteAsync(T entity);
    }
}
