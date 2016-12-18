using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudySite.Data;

namespace StudySite.Services
{
    public class IndexNewsService
    {
        IndexNewsRepository _repository = new IndexNewsRepository();
        public string GetNews()
        {
            return _repository.ReadNewVersionCounters();
        }
    }
}