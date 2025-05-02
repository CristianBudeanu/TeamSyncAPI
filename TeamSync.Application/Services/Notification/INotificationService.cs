using TeamSync.Application.Dto.ChatDtos;

namespace TeamSync.Application.Services;

public interface INotificationService
{
    Task NotifyGroupAsync(Guid projectId, Guid senderId);
    Task<List<ChatNotificationDto>> GetUserNotificationsAsync(string username);
    Task RemoveNotificationAsync(Guid notificationId);
}