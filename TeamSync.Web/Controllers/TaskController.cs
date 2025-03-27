using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Dto.TaskItemDtos;
using TeamSync.Application.Services.TaskServices;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController(ITaskService _taskService) : ControllerBase
    {
        [HttpPost("{projectId}")]
        public async Task<IActionResult> CreateTaskItem(Guid projectId, [FromBody] TaskItemCreateDto dto)
        {
            await _taskService.CreateTaskItem(projectId, dto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaskItemStatus([FromQuery] Guid taskItemId, [FromQuery] string taskItemStatus)
        {
            await _taskService.UpdateTaskItemStatus(taskItemId, taskItemStatus);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUserTasks()
        {
            var tasks = await _taskService.GetAllUserTaskItems();
            return Ok(tasks);
        }
    }
}
