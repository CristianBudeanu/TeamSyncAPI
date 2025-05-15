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
using TeamSync.Helpers.FileHelper;
using TeamSync.Helpers.LoggerHelper;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.ProjectServices
{
    public class ProjectService(
        IFileService imageService,
        IHttpContextService httpContextService,
        IGithubService githubService,
        TeamSyncAppContext context,
        ILoggerHelper _logger
        ) : IProjectService
    {
        public async Task AssignTask(TaskItem task)
        {
            //await _projectRepository.AssignTaskAsync(task);
        }

        public async Task CreateProjectTask(ProjectCreateDto dto)
        {
            var project = dto.Adapt<Project>();
            var user = await context.Users.Where(u => u.Username == httpContextService.GetUsernameFromToken()).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new BadRequestException("User not found!");
            }

            var projectRole = await context.ProjectRoles.FirstOrDefaultAsync(r => r.ProjectRoleName == "Administrator");

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


            if (dto.Image != null)
            {
                var filePath = await imageService.SaveImage(dto.Image, project.Id);
                project.Image = filePath;
            }

            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();
        }

        public async Task<ProjectDto> GetProjectDetails(Guid projectId)
        {
            var user = await context.Users
            .FirstOrDefaultAsync(u => u.Username == httpContextService.GetUsernameFromToken());

            var project = await context.Projects.Where(p => p.Id == projectId)
                .Include(m => m.Members)
                .ThenInclude(t => t.AssignedTasks)
                .ThenInclude(s => s.Status)
                .Include(r => r.GithubRepository)
                .Include(r => r.ProjectUserRoles)
                .ThenInclude(pur => pur.ProjectRole)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                throw new Exception("Project not found or access denied");
            }
            
            foreach (var member in project.Members)
            {
                member.AssignedTasks = member.AssignedTasks
                    .Where(t => t.ProjectId == projectId)
                    .ToList();
            }

            var userRoles = project.ProjectUserRoles
                .Where(pur => pur.UserId == user.Id)
                .Select(pur => pur.ProjectRole.ProjectRoleName)
                .ToList();

            var projectResult = project.Adapt<ProjectDto>();

            projectResult.UserRoles = userRoles;

            // projectResult.UserTasks = userAssignedTasks.Adapt<List<TaskItemDto>>();

            projectResult.Members = project.Members.Select(member => new ProjectUserDto
            {
                Id = member.Id.ToString(),
                Username = member.Username,
                Role = project.ProjectUserRoles
        .FirstOrDefault(pur => pur.UserId == member.Id)?.ProjectRole?.ProjectRoleName ?? "Member",
                AssignedTasks = member.AssignedTasks.Adapt<List<TaskItemDto>>()
            }).ToList();


            if (project.GithubRepository is not null)
            {
                var githubRepo = project.GithubRepository;

                var latestCommits = await githubService.GetRepositoryCommitsTask(
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
            var user = await context.Users.FirstAsync(u => u.Username == httpContextService.GetUsernameFromToken());
            var userProjects = await context.Projects.Where(p => p.Members.Any(m => m.Id == user.Id)).ToListAsync();
            
            return userProjects.Adapt<List<ProjectPreviewDto>>();
        }

        public async Task UpdateProjectWithGithubRepo(Guid projectId, GithubUpdateDto dto)
        {
            var validated = await githubService.ValidateRepositoryCredentialsTask(dto);

            if (validated == false)
            {
                throw new NotFoundException("Github credentials not valid");
            }

            var projectExists = await context.Projects.AnyAsync(p => p.Id == projectId);
            if (!projectExists)
                throw new Exception("Project not found");

            var repo = await context.GithubRepositories
                .FirstOrDefaultAsync(r => r.ProjectId == projectId);
            
            if (repo == null)
            {
                
                repo = new GithubRepository
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    Username = dto.Username,
                    RepositoryName = dto.RepositoryName,
                    Token = dto.Token
                };

                context.GithubRepositories.Add(repo);
            }
            else
            {
                // Update existing GithubRepository
                repo.Username = dto.Username;
                repo.RepositoryName = dto.RepositoryName;
                repo.Token = dto.Token;
            }

            await context.SaveChangesAsync();
        }
    }
}
