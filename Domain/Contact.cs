using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Contact
    {
        [Key]
        public int key { get; set; }

        public string id { get; set; }

        // nickname
        public string name { get; set; }

        // server url
        public string server { get; set;}

        public string last { get; set; } = null;
        public DateTime lastdate { get; set; }

        private int idCounter = 0;

        private ICollection<OurMessage> Messages { get; set; } = new List<OurMessage>();

        public void SendMessage(bool sent, string content)
        {
            OurMessage msg = new OurMessage() {
                id = this.idCounter,
                sent = sent,
                content = content,
                created = DateTime.Now
            };
            Messages.Add(msg);
            last = msg.content;
            lastdate = msg.created;
            idCounter++;
        }

        public OurMessage GetLastMessage()
        {
            if (Messages.Count == 0)
            {
                return null;
            }
            return Messages.Last();
        }

        public OurMessage GetMessage(int messageId)
        {
            foreach (OurMessage message in Messages)
            {
                if (message.id == messageId)
                {
                    return message;
                }
            }
            return null;
        }

        public ICollection<OurMessage> GetAllMessages()
        {
            return Messages;
        }

        public void RemoveMessage(int messageId)
        {
            Messages.Remove(GetMessage(messageId));
        }
    }
}
