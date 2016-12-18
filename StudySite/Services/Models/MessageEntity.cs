using System.Linq;
using StudySite.Data.Models;

namespace StudySite.Models
{
    public class MessageEntity
    {
        public MessageEntity(string name, string text, string date)
        {
            this.name = name;
            this.text = text;
            this.date = date;
        }

        public string name { get; set; }

        public string text { get; set; }

        public string date { get; set; }

        public static MessageEntity[] Convert(Message[] messages)
        {
            return messages.Select(m => new MessageEntity(m.Name, m.Text, m.PartitionKey)).ToArray();
        }
    }
}