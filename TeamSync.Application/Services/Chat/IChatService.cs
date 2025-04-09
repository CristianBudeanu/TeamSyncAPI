using TeamSync.Application.Dto.ChatDtos;

namespace TeamSync.Application.Services.Chat
{
    public interface IChatService
    {
        void AddUserToProject(string username, string connectionId, Guid projectId);
        void RemoveUserFromProject(string connectionId, Guid projectId);
        List<string> GetOnlineUsersByProject(Guid projectId);
        List<Guid> GetProjectsByConnectionId(string connectionId);
        string GetUserByConnectionId(string connectionId);
        Task<List<ChatMessageDto>> GetProjectMessagesTask(Guid projectId);
    }
}
