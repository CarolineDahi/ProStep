using ProStep.Model.Base;
using ProStep.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class NotificationUser : BaseEntity
    {
        public NotificationUser() { }

        public DateTime? ReadDate { get; set; }

        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
