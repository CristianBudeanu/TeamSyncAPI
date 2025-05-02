namespace TeamSync.Domain.Entities.ChatEntities;

public class ChatNotification
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public string ProjectName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}