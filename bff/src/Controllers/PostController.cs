using BFF.Internal;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSW.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSW.Bff.Controllers
{
	[ApiController]
	[Route("bff/[controller]")]
	[AllowAnonymous]
	public class PostController : NswControllerBase
	{

		public PostController(
	IHttpContextAccessor httpContextAccessor,
	IDiscoveryCache discoveryCache,
	ILogger<PostController> logger,
	IConfiguration configuration,
	OidcOptions oidcOptions
) : base(httpContextAccessor, discoveryCache, logger, configuration, oidcOptions) { }



		[HttpGet]
		public async Task<IActionResult> GetPostsAsync()
		{
			var returnValue = await base.GetDataFromApiAsync<List<Post>>("/api/Post", ApiAccessType.Client);
			return Ok(returnValue);
		}
	}
}
