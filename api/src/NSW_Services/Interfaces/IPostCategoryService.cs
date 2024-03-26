using NSW.Data;
using NSW.Data.DTO.Response;

namespace NSW.Services.Interfaces
{
    public interface IPostCategoryService : IService<PostCategory>
    {

        IList<PostCategoryPillResponse> GetPillList();
    }
}
