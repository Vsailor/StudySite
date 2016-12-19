using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using StudySite.Data.Models;

namespace StudySite.Data
{
    public class MessagesRepository
    {
        private CloudStorageAccount _account;
        private CloudTableClient _client;
        private CloudTable _cloudTable;

        public MessagesRepository()
        {
            CloudStorageAccount.TryParse(ConfigurationManager.AppSettings["DataConnection"], out _account);
            _client = _account.CreateCloudTableClient();
            _cloudTable = _client.GetTableReference("Messages");
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
            var result = _cloudTable.ExecuteQuery(tq).ToArray();
            return result;
        }

        public void InsertMessage(Message message)
        {
            var tbo = new TableBatchOperation();
            tbo.Insert(message);
            _cloudTable.ExecuteBatch(tbo);
        }
    }
}