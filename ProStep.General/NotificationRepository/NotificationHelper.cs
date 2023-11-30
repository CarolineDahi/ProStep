using ProStep.DataTransferObject.General.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProStep.General.NotificationRepository
{
    public partial class NotificationRepository
    {
        private async Task<bool> _sendNotification(AddNotificationDto dto)
        {
            var appId = configuration["FCM:StudentAppId"];
            var deviceId = "/topics/all";

            if (!dto.UserIds.Any())
            {
                using (var response = await HttpFCMSender(appId, deviceId, dto))
                {
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            var tokens = context.Users.Where(n => dto.UserIds.Contains(n.Id)).Select(n => n.DeviceToken).ToList();
            using (var response = await HttpFCMMultipleSender(appId, tokens, dto))
            {
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            return true;
        }

        private async Task<HttpResponseMessage> HttpFCMSender(string appId, string deviceId, AddNotificationDto dto)
        {

            var _fcmHttp = httpClient.CreateClient("fcm");

            var Note = new
            {
                to = deviceId,
                //priority = "high",
                notification = new
                {
                    title = dto.Title,
                    body = dto.Body,
                }
            };

            var json = JsonSerializer.Serialize(Note);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            _fcmHttp.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={appId}");

            _fcmHttp.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={appId}");

            return await _fcmHttp.PostAsync("fcm/send", httpContent);
        }

        private async Task<HttpResponseMessage> HttpFCMMultipleSender(string appId, List<string> deviceIds, AddNotificationDto dto)
        {

            var _fcmHttp = httpClient.CreateClient("fcm");

            var Note = new
            {
                registration_ids = deviceIds,
                //priority = "high",
                notification = new
                {
                    title = dto.Title,
                    body = dto.Body,
                }
            };

            var json = JsonSerializer.Serialize(Note);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            _fcmHttp.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={appId}");

            _fcmHttp.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={appId}");

            return await _fcmHttp.PostAsync("fcm/send", httpContent);
        }
    }
}
