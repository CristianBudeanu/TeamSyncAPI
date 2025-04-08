using Microsoft.AspNetCore.Http;

namespace TeamSync.Application.Dto.ProjectDtos
{
    public class ProjectCreateDto
    {
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
