using TeamSync.Application.Dto.GithubDtos;
using TeamSync.Application.Dto.TaskItemDtos;

namespace TeamSync.Application.Dto.ProjectDtos
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Completed { get; set; }
        public List<ProjectUserDto> Members { get; set; } = new List<ProjectUserDto>();
        public List<string> UserRoles { get; set; } = new();
        public GithubRepositoryDto? GithubRepository { get; set; }
        public List<TaskItemDto> UserTasks { get; set; }

    }
}
