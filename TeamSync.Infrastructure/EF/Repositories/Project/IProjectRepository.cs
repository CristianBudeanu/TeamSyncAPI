using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Infrastructure.EF.Repositories.Project
{
    public interface IProjectRepository
    {
        public Task<bool> AssignTaskAsync(TaskItem task);
        public Task<bool> UpdateTask(TaskItem task);

    }
}
