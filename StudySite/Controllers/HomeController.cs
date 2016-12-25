using System.Web.Mvc;

namespace StudySite.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("Error")]
        public ActionResult Error()
        {
            return View("Error");
        }

        [Route("BadCode")]
        public ActionResult BadCode()
        {
            return View("BadCode");
        }

        [Route("Lessons")]
        public ActionResult Lessons()
        {
            return View("Lessons");
        }

        [Route("SmokingRoom")]
        public ActionResult SmokingRoom()
        {
            return View("SmokingRoom");
        }
    }
}
