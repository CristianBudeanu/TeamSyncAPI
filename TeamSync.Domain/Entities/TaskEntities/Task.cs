using System.ComponentModel.DataAnnotations;

namespace TeamSync.Domain.Entities.TaskEntities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public required string TeamName { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
