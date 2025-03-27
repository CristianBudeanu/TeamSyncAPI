using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSync.Application.Dto;
using TeamSync.Application.Dto.GithubDtos;
using TeamSync.Application.Dto.ProjectDtos;
using TeamSync.Application.Services.ProjectServices;

namespace TeamSync.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserProjects()
        {
            return Ok(await projectService.GetUserProjectsTask());
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectDetails(string projectId)
        {
            return Ok(await projectService.GetProjectDetails(Guid.Parse(projectId)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromForm] ProjectCreateDto newProjectData)
        {
            await projectService.CreateProjectTask(newProjectData);
            return Ok(new ServiceResponse { Message = "Good", Status = true });
        }

        [Authorize]
        [HttpPut("{projectId}/github")]
        public async Task<IActionResult> UpdateProjectGithubRepository(Guid projectId, [FromBody] GithubUpdateDto dto)
        {
            await projectService.UpdateProjectWithGithubRepo(projectId, dto);
            return Ok();
        }
    }
}
