using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using StudySite.Data.Abstract;
using StudySite.Data.Models;

namespace StudySite.Data
{
    public class MessagesRepository : AzureTableRepository
    {
        public MessagesRepository() : base("Messages")
        {
        }

        public Message[] GetMessages(int count, string olderThen)
        {
            var tq = new TableQuery<Message>();
            string query = string.Empty;
            if (!string.IsNullOrEmpty(olderThen))
            {
                query = $"PartitionKey gt '{olderThen}'";
            }

            tq.Where(query).Take(count);
            var result = CloudTable.ExecuteQuery(tq).ToArray();
            return result;
        }

        public void InsertMessage(Message message)
        {
            var tbo = new TableBatchOperation();
            tbo.Insert(message);
            CloudTable.ExecuteBatch(tbo);
        }
    }
}