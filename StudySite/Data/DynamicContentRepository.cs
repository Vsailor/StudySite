using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using StudySite.Data.Models;

namespace StudySite.Data
{
    public class DynamicContentRepository
    {
        public string ReadNewVersionCounters()
        {
            CloudStorageAccount account;
            CloudStorageAccount.TryParse(ConfigurationManager.AppSettings["DynamicContentConnection"], out account);
            CloudTableClient client = account.CreateCloudTableClient();
            CloudTable cloudTable = client.GetTableReference("DynamicContent");
            TableQuery<DynamicContentEntity> tq = new TableQuery<DynamicContentEntity>();
            string query = "PartitionKey eq 'Index.News'";
            tq.Where(query).Take(1);
            DynamicContentEntity result = cloudTable.ExecuteQuery(tq).FirstOrDefault();
            return result.Content;
        }
    }
}