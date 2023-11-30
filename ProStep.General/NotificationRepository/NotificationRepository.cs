using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.Notification;
using ProStep.Model.General;
using ProStep.SharedKernel.Enums.General;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.NotificationRepository
{
    public partial class NotificationRepository : ProStepRepository, INotificationRepository
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClient;

        public NotificationRepository(ProStepDBContext context, 
                                      IConfiguration configuration,
                                      IHttpClientFactory httpClient) : base(context)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
        }

        public async Task<OperationResult<IEnumerable<GetNotificationDto>>> GetAll()
        {
            var notifications = await context.NotificationUsers
                                             .Where(n => n.UserId.Equals(context.CurrentUserId()))
                                             .Select(n => new GetNotificationDto
                                             {
                                                 Id = n.NotificationId,
                                                 Title = n.Notification.Title,
                                                 Body = n.Notification.Body,
                                                 IsRead = n.ReadDate.HasValue ? true : false,
                                                 Date = n.ReadDate.Value,
                                                 ImagePath = n.Notification.SenderId.HasValue
                                                              ? n.Notification.Sender.ImagePath : null,
                                             }).ToListAsync();
            return OperationR.SetSuccess(notifications.AsEnumerable());
        }

        public async Task<OperationResult<bool>> MakeAsRead(IEnumerable<Guid> ids)
        {
            var notifications = await context.NotificationUsers
                                             .Where(n => n.UserId.Equals(context.CurrentUserId())
                                                      && ids.Contains(n.NotificationId)
                                                      && !n.ReadDate.HasValue)
                                             .ToListAsync();
            notifications.ForEach(n => n.ReadDate = DateTime.Now);
            context.UpdateRange(notifications);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<IEnumerable<GetNotificationDashDto>>> GetDash()
        {
            var notifications = await context.Notifications
                                             .Select(n => new GetNotificationDashDto
                                             {
                                                 Id = n.Id,
                                                 Title = n.Title,
                                                 Body = n.Body,
                                                 Date = n.DateCreated,
                                                 NotificationFor = n.NotificationFor,
                                                 NotificationType = n.NotificationType,
                                             }).ToListAsync();
            return OperationR.SetSuccess(notifications.AsEnumerable());
        }

        public async Task<OperationResult<GetNotificationDashDto>> GetById(Guid id)
        {
            var notification = await context.Notifications
                                            .Where(n => n.Id.Equals(id))
                                            .Select(n => new GetNotificationDashDto
                                            {
                                                Id = n.Id,
                                                Title = n.Title,
                                                Body = n.Body,
                                                Date = n.DateCreated,
                                                NotificationType = n.NotificationType,
                                                NotificationFor = n.NotificationFor,
                                                Names = n.NotificationUsers
                                                         .Select(u => u.User.FirstName + " " + u.User.LastName)
                                                         .ToList(),
                                            }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(notification);
        }

        public async Task<OperationResult<GetNotificationDashDto>> Add(AddNotificationDto dto)
        {
            var notification = new Notification()
            {
                Title = dto.Title,
                Body = dto.Body,
                SenderId = context.CurrentUserId(),
                NotificationFor = dto.NotificationFor,
                NotificationType = dto.NotificationType,
            };

            switch(dto.NotificationFor)
            {
                case NotificationFor.All :
                    notification.NotificationUsers = context.Users.Where(u => u.UserType != UserType.SuperAdmin)
                                                                  .Select(u => new NotificationUser
                                                                  {
                                                                      UserId = u.Id
                                                                  }).ToList();
                    break;
                case NotificationFor.Admins :
                    notification.NotificationUsers = context.Users.Where(u => u.UserType == UserType.Admin)
                                                                 .Select(u => new NotificationUser
                                                                 {
                                                                     UserId = u.Id
                                                                 }).ToList();
                    break;
                case NotificationFor.Users:
                    notification.NotificationUsers = context.Users.Where(u => u.UserType == UserType.Coach 
                                                                           || u.UserType == UserType.User)
                                                                 .Select(u => new NotificationUser
                                                                 {
                                                                     UserId = u.Id
                                                                 }).ToList();
                    break;
                case NotificationFor.Coach :
                    notification.NotificationUsers = context.Users.Where(u => u.UserType == UserType.Coach)
                                                                 .Select(u => new NotificationUser
                                                                 {
                                                                     UserId = u.Id
                                                                 }).ToList();
                    break;
                case NotificationFor.Student:
                    notification.NotificationUsers = context.Users.Where(u => u.UserType == UserType.User)
                                                                 .Select(u => new NotificationUser
                                                                 {
                                                                     UserId = u.Id
                                                                 }).ToList();
                    break;

                case null:
                    break;

            }

            if(dto.UserIds.Any())
            {
                notification.NotificationUsers = context.Users.Where(u => dto.UserIds.Contains(u.Id))
                                                              .Select(u => new NotificationUser
                                                              {
                                                                  UserId = u.Id
                                                              }).ToList();
            }

            context.Notifications.Add(notification);

            if(!dto.UserIds.Any() && dto.NotificationFor != NotificationFor.All)
            {
                dto.UserIds = notification.NotificationUsers.Select(u => u.UserId).ToList();
            }

            var res = await _sendNotification(dto);
            await context.SaveChangesAsync();

            var notificationUsers = context.Users.Where(u => u.UserType.Equals(dto.NotificationFor)
                                                          && (!dto.UserIds.Any() ? true
                                                                  : dto.UserIds.Contains(u.Id)))
                                                 .Select(s => new NotificationUser
                                                 {
                                                     UserId = s.Id,
                                                     NotificationId = notification.Id,
                                                 }).ToHashSet();
            context.AddRange(notificationUsers);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(notification.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var notifications = await context.Notifications.Where(n =>  ids.Contains(n.Id)).ToListAsync();
            var notificationUsers = await context.NotificationUsers
                                                 .Where(n => ids.Contains(n.NotificationId))
                                                 .ToListAsync();
            context.RemoveRange(notificationUsers);
            context.RemoveRange(notifications);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

    }
}
