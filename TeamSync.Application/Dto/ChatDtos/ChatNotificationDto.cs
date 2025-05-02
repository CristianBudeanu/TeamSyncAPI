namespace TeamSync.Application.Dto.ChatDtos;

public class ChatNotificationDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}