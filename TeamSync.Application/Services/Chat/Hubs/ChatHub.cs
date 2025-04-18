using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TeamSync.Application.Dto.ChatDtos;
using TeamSync.Domain.Entities.ChatEntities;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.Chat.Hubs
{
    public class ChatHub(
        IChatService _chatService,
        TeamSyncAppContext _context
        ) : Hub
    {
        public async Task JoinProjectChat(string username, Guid projectId)
        {
            string groupName = $"ProjectChat-{projectId}";

            _chatService.AddUserToProject(username, Context.ConnectionId, projectId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.SendAsync("UserConnectedToProject", projectId);
            await DisplayProjectOnlineUsers(projectId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var projectIds = _chatService.GetProjectsByConnectionId(Context.ConnectionId);

            foreach (var projectId in projectIds)
            {
                string groupName = $"ProjectChat-{projectId}";
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
                _chatService.RemoveUserFromProject(Context.ConnectionId, projectId);
                await DisplayProjectOnlineUsers(projectId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToProject(Guid projectId, ChatMessageDto messageDto)
        {
            var sender = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == messageDto.FromUsername);

            if (sender == null)
                throw new HubException($"User '{messageDto.FromUsername}' not found.");

            var message = new ChatMessage
            {
                ProjectId = projectId,
                FromId = sender.Id,
                Message = messageDto.Message,
                SentAt = DateTime.UtcNow
            };

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();

            var returnDto = new ChatMessageDto
            {
                FromUsername = sender.Username,
                Message = message.Message,
                SentAt = message.SentAt
            };

            await Clients.Group($"ProjectChat-{projectId}").SendAsync("NewMessage", returnDto);
        }

        private async Task DisplayProjectOnlineUsers(Guid projectId)
        {
            var onlineUsers = _chatService.GetOnlineUsersByProject(projectId);
            string groupName = $"ProjectChat-{projectId}";
            await Clients.Group(groupName).SendAsync("OnlineUsers", onlineUsers);
        }
    }
}
