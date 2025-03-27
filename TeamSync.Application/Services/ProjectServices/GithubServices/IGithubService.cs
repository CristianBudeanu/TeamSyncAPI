using TeamSync.Application.Dto.GithubDtos;

namespace TeamSync.Application.Services.ProjectServices.GithubServices
{
    public interface IGithubService
    {
        Task<List<GithubCommitDto>> GetRepositoryCommitsTask(GithubRepositoryDto dto);
    }
}
