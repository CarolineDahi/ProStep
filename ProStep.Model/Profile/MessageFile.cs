using ProStep.Model.Base;
using ProStep.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.Profile
{
    public class MessageFile : BaseEntity
    {
        public MessageFile() { }

        public Guid MessageId { get; set; }
        public Message Message { get; set; }

        public Guid FileId { get; set; }
        public _File File { get; set; }
    }
}
