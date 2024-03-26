using Microsoft.AspNetCore.Mvc;
using NSW.Api.Controllers.Interfaces;
using NSW.Data;
using NSW.Data.DTO.Response;
using NSW.Services.Interfaces;

namespace NSW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostCategoryController : ControllerBase, IController<PostCategory>
	{
		private readonly IPostCategoryService _service;
		public PostCategoryController(IPostCategoryService service)
		{
			_service = service;
		}


		private ActionResult _delete(PostCategory entity)
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
		public async Task<ActionResult> DeleteAsync([FromBody] PostCategory entity) => await Task.Run(() => this._delete(entity));


		private ActionResult<IList<PostCategory>> _getAll()
		{
			try
			{
				var returnValue = _service.GetAll();
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return Problem(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IList<PostCategory>>> GetAllAsync() => await Task.Run(() => this._getAll());


		private ActionResult<PostCategory?> _getById(int id)
		{
			try
			{
				var returnValue = _service.GetById(id);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return Problem(ex.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PostCategory?>> GetByIdAsync([FromRoute] int id) => await Task.Run(() => this._getById(id));


		private ActionResult<PostCategory?> _getByIdentifier(string identifier)
		{
			try
			{
				var returnValue = _service.GetByIdentifier(identifier);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return Problem(ex.Message);
			}
		}

		[HttpGet("{identifier}")]
		public async Task<ActionResult<PostCategory?>> GetByIdentifierAsync([FromRoute] string identifier) => await Task.Run(() => this._getByIdentifier(identifier));

		private ActionResult<PostCategory> _insert(PostCategory entity)
		{
			try
			{
				var returnValue = _service.Insert(entity);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<PostCategory>> InsertAsync([FromBody] PostCategory entity) => await Task.Run(() => this._insert(entity));

		private ActionResult<PostCategory> _modify([FromBody] PostCategory entity)
		{
			try
			{
				var returnValue = _service.Modify(entity);
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return Problem(ex.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult<PostCategory>> ModifyAsync([FromBody] PostCategory entity) => await Task.Run(() => this._modify(entity));

		private ActionResult<List<PostCategoryPillResponse>> _pillList()
		{
			try
			{
				var currentUser = this.HttpContext.User;
				var returnValue = _service.GetPillList();
				return new OkObjectResult(returnValue);
			}
			catch (Exception ex)
			{
				// add logging
				return Problem(ex.Message);
			}
		}

		[HttpGet("display-list")]
		public async Task<ActionResult<List<PostCategoryPillResponse>>> GetPostCategoryPillListAsync() => await Task.Run(() => this._pillList());
	}
}
