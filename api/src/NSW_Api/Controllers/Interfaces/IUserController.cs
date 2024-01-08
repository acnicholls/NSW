using Microsoft.AspNetCore.Mvc;
using NSW.Data.DTO.Request;
using NSW.Data.DTO.Response;

namespace NSW.Api.Controllers.Interfaces
{
    public interface IUserController
    {
        Task<ActionResult<IList<UserResponse>>> GetAllAsync();
        Task<ActionResult<UserResponse?>> GetByIdAsync(int id);
        Task<ActionResult<UserResponse>> InsertAsync(UserRequest entity);
        Task<ActionResult<UserResponse>> ModifyAsync(UserRequest entity);
        Task<ActionResult> DeleteAsync(UserRequest entity);
    }
}
