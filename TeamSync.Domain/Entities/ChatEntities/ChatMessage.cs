using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Domain.Entities.ChatEntities
{
    public class ChatMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid FromId { get; set; }
        public User FromUser { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
