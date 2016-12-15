using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudySite.Data;

namespace StudySite.Services
{
    public class DynamicContentService
    {
        DynamicContentRepository _repository = new DynamicContentRepository();
        public string GetNews()
        {
            return _repository.ReadNewVersionCounters();
        }
    }
}