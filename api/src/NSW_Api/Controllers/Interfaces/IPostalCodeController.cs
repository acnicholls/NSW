using Microsoft.AspNetCore.Mvc;
using NSW.Data;

namespace NSW.Api.Controllers.Interfaces
{
    public interface IPostalCodeController
    {
        Task<ActionResult<IList<PostalCode>>> GetAllAsync();
        Task<ActionResult<PostalCode?>> GetByIdentifierAsync(string identifier);
        Task<ActionResult<PostalCode>> InsertAsync(PostalCode entity);
        Task<ActionResult<PostalCode>> ModifyAsync(PostalCode entity);
        Task<ActionResult> DeleteAsync(PostalCode entity);
    }
}
