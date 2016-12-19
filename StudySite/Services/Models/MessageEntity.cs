using System;
using System.Linq;
using StudySite.Data.Models;

namespace StudySite.Models
{
    public class MessageEntity
    {
        public MessageEntity(string name, string text, string date, int timeZone)
        {
            this.name = name;
            this.text = text;
            this.date = GetDateTime(int.MaxValue-int.Parse(date)-timeZone*3600).ToString("dd.MM.yyyy, HH:mm:ss");
            unixTime = date;
        }

        public string name { get; set; }

        public string text { get; set; }

        public string date { get; set; }

        public string unixTime { get; set; }

        public static MessageEntity[] Convert(Message[] messages, int timeZone)
        {
            return messages.Select(m => new MessageEntity(m.Name, m.Text, m.PartitionKey, timeZone)).ToArray();
        }

        private DateTime GetDateTime(int dateTimeUnix)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(dateTimeUnix);
        }
    }
}