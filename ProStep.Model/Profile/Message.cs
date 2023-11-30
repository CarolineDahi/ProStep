using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Profile
{
    public class Message : BaseEntity
    {
        public Message() { }

        public string Text { get; set; }
        public DateTime? ReadDate { get; set; }

        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set;}
        //(SenderId == CreaterId) in Conversation
        public ICollection<MessageFile> MessageFiles { get; set; }
    }
}
