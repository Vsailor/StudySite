using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using StudySite.Services;

namespace StudySite.Controllers
{
    public class IndexController : ApiController
    {
        private IndexNewsService _indexNewsService = new IndexNewsService();
        public string Get()
        {
            return _indexNewsService.GetNews();
        }
    }
}
