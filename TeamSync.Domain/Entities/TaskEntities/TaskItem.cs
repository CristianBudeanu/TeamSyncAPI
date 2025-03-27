using System.ComponentModel.DataAnnotations;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Domain.Entities.TaskEntities
{
    public class TaskItem
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public User CreatorUser { get; set; }
        public Guid AssignedTo { get; set; }
        public User AssignedUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid StatusId { get; set; }
        public TaskStatus Status { get; set; }
    }
}
