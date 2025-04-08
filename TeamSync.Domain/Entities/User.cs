using System.ComponentModel.DataAnnotations;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Domain.Entities.TaskEntities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public string PassSalt { get; set; }
        public Guid RoleId { get; set; } // Foreign key 
        public Role Role { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<ProjectUserRole> ProjectUserRoles { get; set; } = [];
        public List<TaskItem> AssignedTasks { get; set; } = [];
    }
}
