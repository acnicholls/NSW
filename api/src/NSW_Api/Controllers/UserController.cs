using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSW.Api.Controllers.Interfaces;
using NSW.Data.DTO.Request;
using NSW.Data.DTO.Response;
using NSW.Services.Interfaces;

namespace NSW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase, IUserController
	{
		private readonly IUserService _service;
		public UserController(IUserService service)
		{
			_service = service;
		}


		private ActionResult _delete(UserRequest entity)
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
		public async Task<ActionResult> DeleteAsync([FromBody] UserRequest entity) => await Task.Run(() => this._delete(entity));


		private ActionResult<IList<UserResponse>> _getAll()
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
		public async Task<ActionResult<IList<UserResponse>>> GetAllAsync() => await Task.Run(() => this._getAll());


		private ActionResult<UserResponse?> _getById(int id)
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
		public async Task<ActionResult<UserResponse?>> GetByIdAsync([FromRoute] int id) => await Task.Run(() => this._getById(id));

		private ActionResult<UserResponse> _insert(UserRequest entity)
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
		public async Task<ActionResult<UserResponse>> InsertAsync([FromBody] UserRequest entity) => await Task.Run(() => this._insert(entity));

		private ActionResult<UserResponse> _modify([FromBody] UserRequest entity)
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
		public async Task<ActionResult<UserResponse>> ModifyAsync([FromBody] UserRequest entity) => await Task.Run(() => this._modify(entity));


#if DEBUG
		[HttpGet("token")]
		[Authorize]
		public async Task<IActionResult> GetMyUserTokenAsync()
		{
			return Ok(await this.HttpContext.GetUserAccessTokenAsync());
		}
#endif

	}
}
