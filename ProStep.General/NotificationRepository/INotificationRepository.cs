using ProStep.DataTransferObject.General.Notification;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<OperationResult<IEnumerable<GetNotificationDto>>> GetAll();
        Task<OperationResult<bool>> MakeAsRead(IEnumerable<Guid> ids);
        Task<OperationResult<IEnumerable<GetNotificationDashDto>>> GetDash();
        Task<OperationResult<GetNotificationDashDto>> GetById(Guid id);
        Task<OperationResult<GetNotificationDashDto>> Add(AddNotificationDto notificationDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
