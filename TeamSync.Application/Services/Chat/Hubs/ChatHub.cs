using Microsoft.AspNetCore.SignalR;
using TeamSync.Application.Dto.ChatDtos;

namespace TeamSync.Application.Services.Chat.Hubs
{
    public class ChatHub(ChatService _chatService) : Hub
    {

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Come2Chat");
            await Clients.Caller.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Come2Chat");
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            _chatService.RemoveUserFromList(user);
            await DisplayOnlineUsers();

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddUserConnectionId(string username)
        {
            _chatService.AddUserConnectionId(username, Context.ConnectionId);
            await DisplayOnlineUsers();
        }

        public async Task ReceiveMessage(ChatMessageDto message)
        {
            await Clients.Groups("Come2Chat").SendAsync("NewMessage", message);
        }

        private async Task DisplayOnlineUsers()
        {
            var onlineUsers = _chatService.GetOnlineUsers();
            await Clients.Groups("Come2Chat").SendAsync("OnlineUsers", onlineUsers);
        }
    }
}
