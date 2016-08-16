using System.Web.Http;

namespace WebApi.Controllers
{
	[RoutePrefix("api/users")]
	public class UsersController : ApiController
	{
		[Route("all")]
		public IHttpActionResult GetAllUsers()
		{
			return Ok("10 users");
		}
	}
}