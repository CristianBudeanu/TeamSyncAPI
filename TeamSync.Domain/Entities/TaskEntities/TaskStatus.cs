using System.ComponentModel.DataAnnotations;

namespace TeamSync.Domain.Entities.TaskEntities
{
    public class TaskStatus
    {
        [Key]
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public List<TaskItem> TaskItems { get; set; }
    }
}
