using System.Linq;
using Microsoft.EntityFrameworkCore;
using TeamSync.Application.Dto.ChatDtos;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.Chat
{
    public class ChatService(TeamSyncAppContext _context) : IChatService
    {
        private static readonly Dictionary<Guid, Dictionary<string, string>> ProjectUsers = new();
        private static readonly Dictionary<string, List<Guid>> ConnectionProjects = new();

        public void AddUserToProject(string username, string connectionId, Guid projectId)
        {
            lock (ProjectUsers)
            {
                if (!ProjectUsers.ContainsKey(projectId))
                    ProjectUsers[projectId] = new Dictionary<string, string>();

                ProjectUsers[projectId][username] = connectionId;

                lock (ConnectionProjects)
                {
                    if (!ConnectionProjects.ContainsKey(connectionId))
                        ConnectionProjects[connectionId] = new List<Guid>();

                    if (!ConnectionProjects[connectionId].Contains(projectId))
                        ConnectionProjects[connectionId].Add(projectId);
                }
            }
        }

        public void RemoveUserFromProject(string connectionId, Guid projectId)
        {
            lock (ProjectUsers)
            {
                if (ProjectUsers.TryGetValue(projectId, out var users))
                {
                    var user = users.FirstOrDefault(x => x.Value == connectionId).Key;
                    if (!string.IsNullOrEmpty(user))
                    {
                        users.Remove(user);
                    }

                    if (users.Count == 0)
                        ProjectUsers.Remove(projectId);
                }

                lock (ConnectionProjects)
                {
                    if (ConnectionProjects.TryGetValue(connectionId, out var projects))
                    {
                        projects.Remove(projectId);
                        if (projects.Count == 0)
                            ConnectionProjects.Remove(connectionId);
                    }
                }
            }
        }

        public List<string> GetOnlineUsersByProject(Guid projectId)
        {
            lock (ProjectUsers)
            {
                if (ProjectUsers.TryGetValue(projectId, out var users))
                {
                    return users.Keys.OrderBy(x => x).ToList();
                }
                return new List<string>();
            }
        }

        public List<Guid> GetProjectsByConnectionId(string connectionId)
        {
            lock (ConnectionProjects)
            {
                if (ConnectionProjects.TryGetValue(connectionId, out var projects))
                {
                    return projects;
                }
                return new List<Guid>();
            }
        }

        public string GetUserByConnectionId(string connectionId)
        {
            lock (ProjectUsers)
            {
                foreach (var project in ProjectUsers)
                {
                    var user = project.Value.FirstOrDefault(x => x.Value == connectionId).Key;
                    if (!string.IsNullOrEmpty(user))
                        return user;
                }
                return null;
            }
        }

        public async Task<List<ChatMessageDto>> GetProjectMessagesTask(Guid projectId)
        {
            var messages = await _context.ChatMessages
            .Where(m => m.ProjectId == projectId)
            .Include(m => m.FromUser)
            .OrderBy(m => m.SentAt)
            .Select(m => new ChatMessageDto
            {
                FromUsername = m.FromUser.Username != null ? m.FromUser.Username : "",
                Message = m.Message,
                SentAt = m.SentAt
            })
            .ToListAsync();

            return messages;
           }
        
    }
}
