using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSW;
using NSW.Data;
using NSW.Data.DTO.Response;
using NSW.Data.Internal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFF.Controllers
{
    [Route("bff/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostCategoryController : ControllerBase
    {
        private readonly IInternalDataTransferService _service;
        public PostCategoryController(IInternalDataTransferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostCategoryAsync()
        {
            var returnValue = await _service.GetDataFromApiAsync<List<PostCategoryPillResponse>>($"/api/PostCategory/display-list", ApiAccessType.Client);
            return Ok(returnValue);
        }


        [HttpGet("id:int")]
        public async Task<IActionResult> GetPostCategoryByIdAsync(int id)
        {
            var returnValue = await _service.GetDataFromApiAsync<PostCategory>($"/api/PostCategory/{id}", ApiAccessType.Client);
            return Ok(returnValue);
        }

    }
}
