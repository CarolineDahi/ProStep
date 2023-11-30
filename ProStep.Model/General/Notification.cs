using ProStep.Model.Base;
using ProStep.Model.Security;
using ProStep.SharedKernel.Enums.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class Notification : BaseEntity
    {
        public Notification() 
        {
            NotificationUsers = new HashSet<NotificationUser>();
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public NotificationFor? NotificationFor { get; set; }
        public NotificationType NotificationType { get; set; }

        public Guid? SenderId { get; set; }
        public User Sender { get; set; }

        public ICollection<NotificationUser> NotificationUsers { get; set; }
    }
}
