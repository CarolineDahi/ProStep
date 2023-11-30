using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.Notification;
using ProStep.General.NotificationRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class NotificationController : Controller
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(List<GetNotificationDashDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() 
            => await notificationRepository.GetAll().ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> MakeAsRead(IEnumerable<Guid> notificationIds)
            => await notificationRepository.MakeAsRead(notificationIds).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(List<GetNotificationDashDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotifications()
            => await notificationRepository.GetDash().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetNotificationDashDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await notificationRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetNotificationDashDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddNotificationDto dto)
            => await notificationRepository.Add(dto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
           => await notificationRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
           => await notificationRepository.Delete(ids).ToJsonResultAsync();
    }
}
