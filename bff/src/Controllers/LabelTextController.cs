using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSW.Data.Internal.Interfaces;
using NSW.Data;
using NSW;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSW.Data.DTO.Response;

namespace BFF.Controllers
{
    [Route("bff/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LabelTextController : ControllerBase
    {
        private readonly IInternalDataTransferService _service;
        public LabelTextController(IInternalDataTransferService service)
        {
            _service = service;
        }



        [HttpGet("group/{identifier}/all")]
        public async Task<IActionResult> GetLabelTextForGroupAsync(string identifier)
        {
            var returnValue = await _service.GetDataFromApiAsync<IDictionary<string, LabelTextDictionaryItemResponse>>($"/api/LabelText/group/{identifier}/all", ApiAccessType.Client);
            return Ok(returnValue);
        }
    }
}
