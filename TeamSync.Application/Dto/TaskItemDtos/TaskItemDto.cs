namespace TeamSync.Application.Dto.TaskItemDtos
{
    public class TaskItemDto
    {
        public Guid Id { get; set; }
        public string ProjectName{ get; set; }
        public string ProjectImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
