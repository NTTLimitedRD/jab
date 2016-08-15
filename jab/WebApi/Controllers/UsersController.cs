using System.Web.Http;

namespace WebApi.Controllers
{
	public class UsersController : ApiController
	{
		public IHttpActionResult GetAllUsers()
		{
			return Ok("hello");
		}
	}
}