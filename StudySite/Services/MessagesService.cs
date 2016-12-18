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
        public string GetMessages(string dateTime, int count)
        {
            List<Message> messages = _messagesRepository.GetMessages(count, dateTime).ToList();
            messages.Sort(delegate(Message m1, Message m2)
            {
                string[] m1Strings = m1.PartitionKey.Split('-');
                string[] m2Strings = m2.PartitionKey.Split('-');

                for (int i = 0; i < m1Strings.Length; i++)
                {
                    var m1Int = int.Parse(m1Strings[i]);
                    var m2Int = int.Parse(m2Strings[i]);
                    if (m1Int < m2Int)
                    {
                        return 1;
                    }
                    else if (m1Int > m2Int)
                    {
                        return -1;
                    }
                }

                return 0;
            });

            return JsonConvert.SerializeObject(MessageEntity.Convert(messages.ToArray()), Formatting.Indented);
        }

        public void InsertMessage(string userName, string text)
        {
            var message = new Message
            {
                Name = userName,
                Text = text,
                PartitionKey = DateTime.UtcNow.ToString("yyyy-M-d-H-m-s-fff"),
                RowKey = string.Empty
            };

            _messagesRepository.InsertMessage(message);
        }
    }
}