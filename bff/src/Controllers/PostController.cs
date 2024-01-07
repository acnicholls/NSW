using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSW.Data;
using NSW.Data.Internal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSW.Bff.Controllers
{
	[ApiController]
	[Route("bff/[controller]")]
	[AllowAnonymous]
	public class PostController : ControllerBase
	{
		private readonly IInternalDataTransferService _service;
		public PostController(IInternalDataTransferService service) 
		{
			_service = service;
		}



		[HttpGet]
		public async Task<IActionResult> GetPostsAsync()
		{
			var returnValue = await _service.GetDataFromApiAsync<List<Post>>("/api/Post", ApiAccessType.Client);
			return Ok(returnValue);
		}

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPostAsync(int id)
        {
            var returnValue = await _service.GetDataFromApiAsync<Post>($"/api/Post/{id}", ApiAccessType.Client);
            return Ok(returnValue);
        }
    }
}
