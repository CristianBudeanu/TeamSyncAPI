using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Dto.ChatDtos;
using TeamSync.Application.Services;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<List<ChatNotificationDto>> GetUserNotifications(string username)
        {
            return await _notificationService.GetUserNotificationsAsync(username);
        }
        
        [Authorize]
        [HttpDelete]
        public async Task DeleteViewedNotification(Guid notificationId)
        {
            await _notificationService.RemoveNotificationAsync(notificationId);
        }
    }
}
