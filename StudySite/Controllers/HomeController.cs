using System.Web.Mvc;

namespace StudySite.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("{pageName}")]
        public ActionResult Index(string pageName)
        {

            if (pageName != null)
            {
                return View(pageName); // Redirect("View/Index.html");
            }
            else
            {
                return View();
            }
        }
    }
}
