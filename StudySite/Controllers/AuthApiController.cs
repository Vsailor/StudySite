using System.Web.Http;
using StudySite.Services;
using StudySite.Services.Models;

namespace StudySite.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthApiController : ApiController
    {
        UserService _userService = new UserService();

        [HttpGet]
        [Route("{guid}")]
        public UserEntity Get(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return null;
            }

            return _userService.GetUser(guid);
        }
    }
}
