using NSW.Data.DTO.Request;
using NSW.Data.DTO.Response;

namespace NSW.Services.Interfaces
{
    public interface IUserService 
	{
        IList<UserResponse> GetAll();
        UserResponse? GetById(int id);
        UserResponse GetByEmail(string email);

		bool ExistsByEmail(string email);
        UserResponse Insert(UserRequest request);
        UserResponse Modify(UserRequest entity);
        void Delete(UserRequest entity);
    }
}
