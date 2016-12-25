using System.Web.Http;
using StudySite.Services;

namespace StudySite.Controllers
{
    [RoutePrefix("api/Index")]
    public class IndexController : ApiController
    {
        private IndexNewsService _indexNewsService = new IndexNewsService();

        [HttpGet]
        [Route("")]
        public string Get()
        {
            return _indexNewsService.GetNews();
        }
    }
}
