using System.ComponentModel.DataAnnotations;
using TeamSync.Domain.Entities.ChatEntities;
using TeamSync.Domain.Entities.GithubEntities;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Domain.Entities.ProjectEntities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Completed { get; set; }
        public DateTime CreationDate { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        public List<ProjectUserRole> ProjectUserRoles { get; set; } = [];
        public Invitation? Invitation { get; set; }
        public GithubRepository? GithubRepository { get; set; }
        public List<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
        public List<ChatMessage> chatMessages { get; set; } = new List<ChatMessage>();
    }
}
