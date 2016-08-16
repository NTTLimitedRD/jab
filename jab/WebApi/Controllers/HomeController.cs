using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowAnonymous]
	[RoutePrefix("api/home")]
	public class HomeController : ApiController
	{
        [HttpGet]
		[Route("hello")]
		public IHttpActionResult hello()
		{
			return Ok("hello");
		}
	}
}