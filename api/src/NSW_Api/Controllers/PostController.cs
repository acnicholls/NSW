using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSW.Api.Controllers.Interfaces;
using NSW.Data;
using NSW.Services.Interfaces;

namespace NSW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase, IController<Post>
	{
		private readonly IService<Post> _service;
		public PostController(IService<Post> service)
		{
			_service = service;
		}


		private ActionResult _delete(Post entity)
		{
			try
			{
				_service.Delete(entity);
				return Ok();
			}
			catch (Exception ex)
			{
				// add logging
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteAsync([FromBody] Post entity) => await Task.Run(() => this._delete(entity));


		private ActionResult<IList<Post>> _getAll()
		{
			try
			{
				var returnValue = _service.GetAll();
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IList<Post>>> GetAllAsync() => await Task.Run(() => this._getAll());


		private ActionResult<Post?> _getById(int id)
		{
			try
			{
				var returnValue = _service.GetById(id);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Post?>> GetByIdAsync([FromQuery] int id) => await Task.Run(() => this._getById(id));


		private ActionResult<Post?> _getByIdentifier(string identifier)
		{
			try
			{
				var returnValue = _service.GetByIdentifier(identifier);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{identifier}")]
		public async Task<ActionResult<Post?>> GetByIdentifierAsync([FromQuery] string identifier) => await Task.Run(() => this._getByIdentifier(identifier));

		private ActionResult<Post> _insert(Post entity)
		{
			try
			{
				var returnValue = _service.Insert(entity);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<Post>> InsertAsync([FromBody] Post entity) => await Task.Run(() => this._insert(entity));

		private ActionResult<Post> _modify([FromBody] Post entity)
		{
			try
			{
				var returnValue = _service.Modify(entity);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult<Post>> ModifyAsync([FromBody] Post entity) => await Task.Run(() => this._modify(entity));
	}
}
