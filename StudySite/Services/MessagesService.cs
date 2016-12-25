using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StudySite.Data;
using StudySite.Data.Models;
using StudySite.Models;

namespace StudySite.Services
{
    public class MessagesService
    {
        MessagesRepository _messagesRepository = new MessagesRepository();
        UserRepository _userRepository = new UserRepository();
        public string GetMessages(string dateTime, int count, int timeZone)
        {
            List<Message> messages = _messagesRepository.GetMessages(count, dateTime).ToList();
            messages.Sort(delegate(Message m1, Message m2)
            {
                int partKeyM1 = int.MaxValue - int.Parse(m1.PartitionKey);
                int partKeyM2 = int.MaxValue - int.Parse(m2.PartitionKey);

                return partKeyM2 < partKeyM1 ? -1 : 1;
            });

            var messagesUsers = new Dictionary<Message, User>();
            foreach (var message in messages)
            {
                var existUser = messagesUsers.FirstOrDefault(m => m.Value.PartitionKey == message.RowKey);
                if (existUser.Key != null)
                {
                    messagesUsers.Add(message, existUser.Value);
                    continue;
                }

                User user = _userRepository.GetUser(message.RowKey);
                messagesUsers.Add(message, user);
            }

            return JsonConvert.SerializeObject(MessageEntity.Convert(messagesUsers, timeZone), Formatting.Indented);
        }

        public void InsertMessage(string userGuid, string text)
        {
            User user = _userRepository.GetUser(userGuid);
            if (user == null)
            {
                throw new ArgumentException("Not found");
            }

            var message = new Message
            {
                Text = text,
                PartitionKey = (int.MaxValue - TimeService.GetUnixTime(DateTime.UtcNow)).ToString(),
                RowKey = userGuid
            };

            _messagesRepository.InsertMessage(message);
        }
    }
}