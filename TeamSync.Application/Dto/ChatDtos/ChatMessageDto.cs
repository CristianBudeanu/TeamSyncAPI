using System.ComponentModel.DataAnnotations;

namespace TeamSync.Application.Dto.ChatDtos
{
    public class ChatMessageDto
    {
        [Required]
        public string From { get; set; }
        public string To { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
