using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using System.Runtime.Serialization;

namespace StudySite.Data.Models
{
    public class Message : TableEntity
    {
        public string Name { get; set; }

        public string Text { get; set; }
    }
}