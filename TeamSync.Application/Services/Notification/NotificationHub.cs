using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services;

public class NotificationHub : Hub
{
    private readonly TeamSyncAppContext _context;
    
    public NotificationHub(TeamSyncAppContext context)
    {
        _context = context;
    }
    
    public async Task RegisterUser(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{user.Id}");
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Optional: Clean-up logic
        await base.OnDisconnectedAsync(exception);
    }
}