using System;
using System.Collections.Generic;
using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Infrastructure.EF.Repositories.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _context;

        public ProjectRepository(ProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> AssignTaskAsync(TaskItem task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);

                int result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTask(TaskItem task)
        {
            throw new NotImplementedException();
        }
    }
}
