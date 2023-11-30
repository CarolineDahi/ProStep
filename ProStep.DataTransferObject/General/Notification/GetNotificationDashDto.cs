using ProStep.SharedKernel.Enums.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Notification
{
    public class GetNotificationDashDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? Date { get; set; }
        public bool IsRead { get; set; }
        public List<string> Names { get; set; }
        public NotificationFor? NotificationFor { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
