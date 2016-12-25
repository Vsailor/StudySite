using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace StudySite.Data.Models
{
    public class Message : TableEntity
    {
        public string Text { get; set; }
    }
}