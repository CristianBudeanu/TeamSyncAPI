
using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Infrastructure.EF.Repositories.Project;

namespace TeamSync.Application.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task AssignTask(TaskItem task)
        {
            await _projectRepository.AssignTaskAsync(task);
        }
    }
}
