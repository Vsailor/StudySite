using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using Newtonsoft.Json;
using StudySite.Data;
using StudySite.Data.Models;
using StudySite.Models;

namespace StudySite.Services
{
    public class MessagesService
    {
        MessagesRepository _messagesRepository = new MessagesRepository();
        public string GetMessages(string dateTime, int count, int timeZone)
        {
            List<Message> messages = _messagesRepository.GetMessages(count, dateTime).ToList();
            messages.Sort(delegate(Message m1, Message m2)
            {
                int partKeyM1 = int.MaxValue - int.Parse(m1.PartitionKey);
                int partKeyM2 = int.MaxValue - int.Parse(m2.PartitionKey);

                return partKeyM2 < partKeyM1 ? -1 : 1;
            });

            return JsonConvert.SerializeObject(MessageEntity.Convert(messages.ToArray(), timeZone), Formatting.Indented);
        }

        public void InsertMessage(string userName, string text)
        {
            var message = new Message
            {
                Name = userName,
                Text = text,
                PartitionKey = (int.MaxValue - GetUnixTime(DateTime.UtcNow)).ToString(),
                RowKey = string.Empty
            };

            _messagesRepository.InsertMessage(message);
        }

        private int GetUnixTime(DateTime dateTime)
        {
            return (int)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }
    }
}