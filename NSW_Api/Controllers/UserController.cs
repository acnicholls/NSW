﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSW.Api.Controllers.Interfaces;
using NSW.Data;
using NSW.Services.Interfaces;

namespace NSW.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase, IController<User>
	{
		private readonly IService<User> _service;
		public UserController(IService<User> service)
		{
			_service = service;
		}


		private ActionResult _delete(User entity)
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
		public async Task<ActionResult> DeleteAsync([FromBody] User entity) => await Task.Run(() => this._delete(entity));


		private ActionResult<IList<User>> _getAll()
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
		public async Task<ActionResult<IList<User>>> GetAllAsync() => await Task.Run(() => this._getAll());


		private ActionResult<User?> _getById(int id)
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
		public async Task<ActionResult<User?>> GetByIdAsync([FromQuery] int id) => await Task.Run(() => this._getById(id));


		private ActionResult<User?> _getByIdentifier(string identifier)
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
		public async Task<ActionResult<User?>> GetByIdentifierAsync([FromQuery] string identifier) => await Task.Run(() => this._getByIdentifier(identifier));

		private ActionResult<User> _insert(User entity)
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
		public async Task<ActionResult<User>> InsertAsync([FromBody] User entity) => await Task.Run(() => this._insert(entity));

		private ActionResult<User> _modify([FromBody] User entity)
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
		public async Task<ActionResult<User>> ModifyAsync([FromBody] User entity) => await Task.Run(() => this._modify(entity));
	}
}