using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace StudySite.Data.Models
{
    public class IndexNews : TableEntity
    {
        public string Content { get; set; }
    }
}