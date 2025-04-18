using Mapster;
using Microsoft.EntityFrameworkCore;
using TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions;
using TeamSync.Application.Dto.TaskItemDtos;
using TeamSync.Domain.Entities.TaskEntities;
using TeamSync.Helpers.HttpContextHelper;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.TaskServices
{
    public class TaskService : ITaskService
    {
        private readonly TeamSyncAppContext _context;
        private readonly IHttpContextService _httpContextService;

        public TaskService(TeamSyncAppContext context, IHttpContextService httpContextService)
        {
            _context = context;
            _httpContextService = httpContextService;
        }
        public async Task CreateTaskItem(Guid projectId, TaskItemCreateDto dto)
        {
            if (!await _context.Projects.AnyAsync(pid => pid.Id == projectId))
            {
                throw new NotFoundException("Project with id: " + projectId + " not found.");
            }

            //if(await _context.Tasks.Where(pid => pid.ProjectId == projectId).AnyAsync(uid => uid.AssignedTo == dto.AssignedTo))
            //{
            //    throw new BadRequestException("User has a task.");
            //}

            var taskItem = dto.Adapt<TaskItem>();

            var creator = await _context.Users.Where(u => u.Username == _httpContextService.GetUsernameFromToken()).FirstOrDefaultAsync();
            taskItem.StatusId = (await _context.TaskStatuses.Where(u => u.StatusName == "Pending").FirstOrDefaultAsync()).Id;

            taskItem.ProjectId = projectId;
            taskItem.CreatedBy = creator.Id;

            await _context.Tasks.AddAsync(taskItem);
            await _context.SaveChangesAsync();

        }

        public Task DeleteTaskItem()
        {
            throw new BadRequestException("User has a task.");
        }

        public async Task<List<TaskItemDto>> GetAllUserTaskItems()
        {
            var username = _httpContextService.GetUsernameFromToken();
            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("User not found!");
            }

            return (
                await _context.Tasks
                .Include(s => s.Status)
                .Include(p => p.Project)
                .Where(t => t.AssignedTo == user.Id)
                .ToListAsync()
                ).Adapt<List<TaskItemDto>>();
        }

        public Task UpdateTaskItem()
        {
            throw new BadRequestException("User has a task.");
        }

        public async Task UpdateTaskItemStatus(Guid taskId, string status)
        {
            var task = await _context.Tasks.Include(a => a.AssignedUser).Where(t => t.Id == taskId).FirstOrDefaultAsync();

            if (task == null)
            {
                throw new NotFoundException("Task not found!");
            }

            var taskStatus = await _context.TaskStatuses.Where(s => s.StatusName == status).FirstOrDefaultAsync();

            if (taskStatus == null)
            {
                throw new NotFoundException("Task status not found!");
            }

            task.StatusId = taskStatus.Id;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
