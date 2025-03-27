namespace TeamSync.Application.Dto.TaskItemDtos
{
    public class TaskItemCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public Guid CreatedBy { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
