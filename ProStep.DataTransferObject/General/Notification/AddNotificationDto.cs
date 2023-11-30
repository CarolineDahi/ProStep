using ProStep.SharedKernel.Enums.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Notification
{
    public class AddNotificationDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public NotificationFor? NotificationFor { get; set; }
        public NotificationType NotificationType { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
