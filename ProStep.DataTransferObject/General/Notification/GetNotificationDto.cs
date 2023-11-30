using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.DataTransferObject.General.Notification
{
    public class GetNotificationDto : GetNotificationDashDto
    {
        public bool IsRead { get; set; }
        public string ImagePath { get; set; }
    }
}
