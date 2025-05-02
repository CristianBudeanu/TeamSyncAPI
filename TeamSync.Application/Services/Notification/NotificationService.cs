using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions;
using TeamSync.Application.Dto.ChatDtos;
using TeamSync.Domain.Entities.ChatEntities;
using TeamSync.Infrastructure.EF.Contexts;
using NotImplementedException = System.NotImplementedException;

namespace TeamSync.Application.Services;

public class NotificationService : INotificationService
{
    private readonly TeamSyncAppContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(TeamSyncAppContext context, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }
    
    
    public async Task NotifyGroupAsync(Guid projectId, Guid senderId)
    {
        var project = await _context.Projects
            .Include(p => p.Members)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
            throw new NotFoundException("Project not found");

        var notificationsToAdd = new List<ChatNotification>();

        foreach (var member in project.Members)
        {
            // Skip sender
            if (member.Id == senderId)
                continue;

            bool alreadyNotified = await _context.ChatNotifications
                .AnyAsync(n => n.UserId == member.Id && n.ProjectId == projectId);

            if (!alreadyNotified)
            {
                var notification = new ChatNotification
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    UserId = member.Id,
                    ProjectName = project.Name,
                };

                notificationsToAdd.Add(notification);
            }
        }

        if (notificationsToAdd.Any())
        {
            await _context.ChatNotifications.AddRangeAsync(notificationsToAdd);
            await _context.SaveChangesAsync();

            // Send all in parallel
            foreach (var notification in notificationsToAdd)
            {
                await _hubContext.Clients
                    .Group($"user-{notification.UserId}")
                    .SendAsync("NewMessageNotification", new
                    {
                        notification.Id,
                        notification.ProjectId,
                        notification.ProjectName,
                    });
            }
        }
    }

    public async Task<List<ChatNotificationDto>> GetUserNotificationsAsync(string username)
    {

        var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var notifications = await _context.ChatNotifications
            .Where(n => n.UserId == user.Id)
            .ToListAsync();

        return notifications.Adapt<List<ChatNotificationDto>>();
    }

    public async Task RemoveNotificationAsync(Guid notificationId)
    {
        var notif = await _context.ChatNotifications
            .FirstOrDefaultAsync(n => n.Id == notificationId);

        if (notif != null)
        {
            _context.ChatNotifications.Remove(notif);
            await _context.SaveChangesAsync();
        }
    }
}