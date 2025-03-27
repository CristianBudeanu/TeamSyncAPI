using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Domain.Entities.ProjectEntities
{
    public class ProjectUserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
