using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Domain.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public Invitation(Guid projectId)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            Token = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddHours(1);
        }
    }
}
