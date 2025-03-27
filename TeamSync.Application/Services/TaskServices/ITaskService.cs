using TeamSync.Application.Dto.TaskItemDtos;

namespace TeamSync.Application.Services.TaskServices
{
    public interface ITaskService
    {
        Task CreateTaskItem(Guid projectId, TaskItemCreateDto dto);
        Task UpdateTaskItemStatus(Guid taskId, string status);
        Task UpdateTaskItem();
        Task DeleteTaskItem();
        Task<List<TaskItemDto>> GetAllUserTaskItems();
    }
}
