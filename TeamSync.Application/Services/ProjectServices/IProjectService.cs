﻿using TeamSync.Application.Dto.GithubDtos;
using TeamSync.Application.Dto.ProjectDtos;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Application.Services.ProjectServices
{
    public interface IProjectService
    {
        Task CreateProjectTask(ProjectCreateDto dto);
        Task<List<ProjectPreviewDto>> GetUserProjectsTask();
        Task<ProjectDto> GetProjectDetails(Guid projectId);
        Task UpdateProjectWithGithubRepo(Guid projectId, GithubUpdateDto dto);
    }
}
