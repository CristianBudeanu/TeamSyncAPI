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
        var project = await _context.Projects.Include(m => m.Members).FirstOrDefaultAsync(p => p.Id == projectId);
        if (project == null)
        {
            throw new NotFoundException("Project not found");
        }

        foreach (var member in project.Members)
        {
            var exists = await _context.ChatNotifications.AnyAsync(u => u.UserId == member.Id && u.ProjectId == projectId);

            if (!exists)
            {
                var notification = new ChatNotification
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    UserId = member.Id,
                    ProjectName = project.Name,
                };
                
                await _context.ChatNotifications.AddAsync(notification);
                
                await _hubContext.Clients.Group($"user-{member.Id}")
                    .SendAsync("NewMessageNotification", new
                    {
                        NotificationId = notification.Id,
                        ProjectId = projectId,
                        ProjectName = project.Name
                    });
            }
        }
        await _context.SaveChangesAsync();
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
            .Select(n => new ChatNotification
            {
                ProjectId = n.ProjectId,
                ProjectName = n.ProjectName,
                CreatedAt = n.CreatedAt
            })
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