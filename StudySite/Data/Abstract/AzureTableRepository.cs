using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace StudySite.Data.Abstract
{
    public class AzureTableRepository
    {
        protected CloudStorageAccount Account;
        protected CloudTableClient Client { get; set; }
        protected CloudTable CloudTable { get; set; }

        public AzureTableRepository(string tableName)
        {
            CloudStorageAccount.TryParse(ConfigurationManager.AppSettings["DataConnection"], out Account);
            Client = Account.CreateCloudTableClient();
            CloudTable = Client.GetTableReference(tableName);
        }

        public void Insert(ITableEntity tableEntity)
        {
            var tbo = new TableBatchOperation();
            tbo.Insert(tableEntity);
            CloudTable.ExecuteBatch(tbo);
        }
    }
}