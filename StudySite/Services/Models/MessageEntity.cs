using System.Collections.Generic;
using System.Linq;
using StudySite.Data.Models;
using StudySite.Services;

namespace StudySite.Models
{
    public class MessageEntity
    {
        public MessageEntity(string text, string date, int timeZone, string picture, string name)
        {
            this.name = name;
            this.picture = picture;
            this.text = text;
            this.date = TimeService.GetDateTime(int.MaxValue-int.Parse(date)-timeZone*3600).ToString("dd.MM.yyyy, HH:mm:ss");
            unixTime = date;
        }

        public string name { get; set; }

        public string picture { get; set; }

        public string text { get; set; }

        public string date { get; set; }

        public string unixTime { get; set; }

        public static MessageEntity[] Convert(Dictionary<Message, User> messagesUsers, int timeZone)
        {
            return messagesUsers.Select(m => new MessageEntity(m.Key.Text, m.Key.PartitionKey, timeZone, m.Value.PictureUrl, m.Value.FullName)).ToArray();
        }
    }
}