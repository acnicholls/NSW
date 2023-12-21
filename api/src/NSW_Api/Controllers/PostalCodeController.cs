using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSW.Api.Controllers.Interfaces;
using NSW.Data;
using NSW.Services;
using NSW.Services.Interfaces;

namespace NSW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostalCodeController : ControllerBase, IController<PostalCode>
	{
		private readonly IService<PostalCode> _service;
		public PostalCodeController(IService<PostalCode> service)
		{
			_service = service;
		}


		private ActionResult _delete(PostalCode entity)
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
		public async Task<ActionResult> DeleteAsync([FromBody] PostalCode entity) => await Task.Run(() => this._delete(entity));


		private ActionResult<IList<PostalCode>> _getAll()
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
		public async Task<ActionResult<IList<PostalCode>>> GetAllAsync() => await Task.Run(() => this._getAll());


		private ActionResult<PostalCode?> _getById(int id)
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
		public async Task<ActionResult<PostalCode?>> GetByIdAsync([FromRoute] int id) => await Task.Run(() => this._getById(id));


		private ActionResult<PostalCode?> _getByIdentifier(string identifier)
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
		public async Task<ActionResult<PostalCode?>> GetByIdentifierAsync([FromRoute] string identifier) => await Task.Run(() => this._getByIdentifier(identifier));

		private ActionResult<PostalCode> _insert(PostalCode entity)
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
		public async Task<ActionResult<PostalCode>> InsertAsync([FromBody] PostalCode entity) => await Task.Run(() => this._insert(entity));

		private ActionResult<PostalCode> _modify([FromBody] PostalCode entity)
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
		public async Task<ActionResult<PostalCode>> ModifyAsync([FromBody] PostalCode entity) => await Task.Run(() => this._modify(entity));
	}
}
