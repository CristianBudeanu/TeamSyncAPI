using System.ComponentModel.DataAnnotations;

namespace TeamSync.Application.Dto.ChatDtos
{
    public class ChatMessageDto
    {
        public string FromUsername { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}
