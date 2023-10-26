using Microsoft.AspNetCore.Mvc;
using NSW.Api.Controllers.Interfaces;
using NSW.Data;
using NSW.Services.Interfaces;

namespace NSW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LabelTextController : ControllerBase, IController<LabelText>
	{
		private readonly IService<LabelText> _service;
		public LabelTextController(IService<LabelText> service)
		{
			_service = service;
		}


		private ActionResult _delete(LabelText entity)
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
		public async Task<ActionResult> DeleteAsync([FromBody] LabelText entity) => await Task.Run(() => this._delete(entity));


		private ActionResult<IList<LabelText>> _getAll()
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
		public async Task<ActionResult<IList<LabelText>>> GetAllAsync() => await Task.Run(() => this._getAll());


		private ActionResult<LabelText?> _getById(int id)
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
		public async Task<ActionResult<LabelText?>> GetByIdAsync([FromQuery] int id) => await Task.Run(() => this._getById(id));


		private ActionResult<LabelText?> _getByIdentifier(string identifier)
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
		public async Task<ActionResult<LabelText?>> GetByIdentifierAsync([FromQuery] string identifier) => await Task.Run(() => this._getByIdentifier(identifier));

		private ActionResult<LabelText> _insert(LabelText entity)
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
		public async Task<ActionResult<LabelText>> InsertAsync([FromBody] LabelText entity) => await Task.Run(() => this._insert(entity));

		private ActionResult<LabelText> _modify([FromBody] LabelText entity)
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
		public async Task<ActionResult<LabelText>> ModifyAsync([FromBody] LabelText entity) => await Task.Run(() => this._modify(entity));
	}
}
