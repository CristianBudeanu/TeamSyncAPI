using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Services.Project;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        [HttpPost]
        public async Task AssignTaskToUser(TaskItem task)
        {
            await projectService.AssignTask(task);
        }
    }
}
