using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using StudySite.Services;

namespace StudySite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IndexController : ApiController
    {
        private DynamicContentService _dynamicContentService = new DynamicContentService();
        public string Get()
        {
            return _dynamicContentService.GetNews();
        }
    }
}
