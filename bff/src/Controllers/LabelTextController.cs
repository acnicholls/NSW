using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSW;
using NSW.Data.DTO.Response;
using NSW.Data.Internal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
