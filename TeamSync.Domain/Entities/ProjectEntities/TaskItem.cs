using System.ComponentModel.DataAnnotations;
using TeamSync.Domain.Enums;

namespace TeamSync.Domain.Entities.ProjectEntities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Requester {  get; set; }
        public string? Executor { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskState Status { get; set; }

    }
}
