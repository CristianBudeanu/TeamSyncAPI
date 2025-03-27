using Mapster;
using Microsoft.EntityFrameworkCore;
using TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions;
using TeamSync.Application.Dto.GithubDtos;
using TeamSync.Application.Dto.ProjectDtos;
using TeamSync.Application.Dto.TaskItemDtos;
using TeamSync.Application.Services.ProjectServices.GithubServices;
using TeamSync.Domain.Entities.GithubEntities;
using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Domain.Entities.TaskEntities;
using TeamSync.Helpers.HttpContextHelper;
using TeamSync.Helpers.ImageHelper;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.ProjectServices
{
    public class ProjectService(
        IImageService _imageService,
        IHttpContextService _httpContextService,
        IGithubService _githubService,
        TeamSyncAppContext _context
        ) : IProjectService
    {
        public async Task AssignTask(TaskItem task)
        {
            //await _projectRepository.AssignTaskAsync(task);
        }

        public async Task CreateProjectTask(ProjectCreateDto dto)
        {
            var project = dto.Adapt<Domain.Entities.ProjectEntities.Project>();
            var user = await _context.Users.Where(u => u.Username == _httpContextService.GetUsernameFromToken()).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new BadRequestException("User not found!");
            }

            var projectRole = await _context.ProjectRoles.FirstOrDefaultAsync(r => r.ProjectRoleName == "Administrator");

            if (projectRole == null)
            {
                throw new BadRequestException("Project role not found!");
            }

            project.Members.Add(user);

            var projectUserRole = new ProjectUserRole
            {
                UserId = user.Id,
                User = user,
                ProjectId = project.Id,
                Project = project,
                ProjectRoleId = projectRole.Id,
                ProjectRole = projectRole,
                AssignedAt = DateTime.UtcNow,
            };

            project.ProjectUserRoles.Add(projectUserRole);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            //var projectRole = _projectRepository.

            //if (projectData.Image != null)
            //{
            //    var projectList = await _projectRepository.GetProjectsAsync();

            //    if (projectList != null) 
            //    {
            //        var lastCreatedProject = projectList.LastOrDefault();
            //        project.Image = "image" + lastCreatedProject.Id;
            //        await _imageService.SaveImage(projectData.Image);
            //    }
            //    else
            //    {
            //        project.Image = "image1";
            //    }
            //}
        }

        public async Task<ProjectDto> GetProjectDetails(Guid projectId)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == _httpContextService.GetUsernameFromToken());

            var project = await _context.Projects.Where(p => p.Id == projectId)
                .Include(m => m.Members)
                .Include(r => r.GithubRepository)
                .Include(r => r.ProjectUserRoles)
                .ThenInclude(pur => pur.ProjectRole)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                throw new Exception("Project not found or access denied");
            }

            var userAssignedTasks = await _context.Tasks
                .Include(s => s.Status)
            .Where(t => t.ProjectId == projectId && t.AssignedTo == user.Id)
            .ToListAsync();

            var userRoles = project.ProjectUserRoles
                .Where(pur => pur.UserId == user.Id)
                .Select(pur => pur.ProjectRole.ProjectRoleName)
                .ToList();

            var projectResult = project.Adapt<ProjectDto>();

            projectResult.UserRoles = userRoles;

            projectResult.UserTasks = userAssignedTasks.Adapt<List<TaskItemDto>>();

            if (project.GithubRepository is not null)
            {
                var githubRepo = project.GithubRepository;

                var latestCommits = await _githubService.GetRepositoryCommitsTask(
                    new GithubRepositoryDto
                    {
                        Username = githubRepo.Username,
                        RepositoryName = githubRepo.RepositoryName,
                        Token = githubRepo.Token
                    }
                );

                projectResult.GithubRepository.GithubCommits = latestCommits;
            }
            return projectResult;
        }

        public async Task<List<ProjectPreviewDto>> GetUserProjectsTask()
        {
            var user = await _context.Users.FirstAsync(u => u.Username == _httpContextService.GetUsernameFromToken());
            var userProjects = await _context.Projects.Where(p => p.Members.Any(m => m.Id == user.Id)).ToListAsync();


            return userProjects.Adapt<List<ProjectPreviewDto>>();
        }

        public async Task UpdateProjectWithGithubRepo(Guid projectId, GithubUpdateDto dto)
        {
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectId);
            if (!projectExists)
                throw new Exception("Project not found");

            var repo = await _context.GithubRepositories
                .FirstOrDefaultAsync(r => r.ProjectId == projectId);

            if (repo == null)
            {
                // Create new GithubRepository
                repo = new GithubRepository
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    Username = dto.Username,
                    RepositoryName = dto.RepositoryName,
                    Token = dto.Token
                };

                _context.GithubRepositories.Add(repo);
            }
            else
            {
                // Update existing GithubRepository
                repo.Username = dto.Username;
                repo.RepositoryName = dto.RepositoryName;
                repo.Token = dto.Token;
            }

            await _context.SaveChangesAsync();
        }
    }
}
