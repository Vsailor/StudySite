using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using StudySite.Data.Abstract;
using StudySite.Data.Models;

namespace StudySite.Data
{
    public class IndexNewsRepository : AzureTableRepository
    {
        public string ReadNewVersionCounters()
        {
            var tq = new TableQuery<IndexNews>();
            string query = "PartitionKey eq 'Index.News'";
            tq.Where(query).Take(1);
            IndexNews result = CloudTable.ExecuteQuery(tq).FirstOrDefault();
            return result.Content;
        }

        public IndexNewsRepository() : base("DynamicContent")
        {
        }
    }
}