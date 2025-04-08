using Octokit;
using TeamSync.Application.Dto.GithubDtos;

namespace TeamSync.Application.Services.ProjectServices.GithubServices
{
    public class GithubService : IGithubService
    {

        public async Task<List<GithubCommitDto>> GetRepositoryCommitsTask(GithubRepositoryDto dto)
        {
            List<GithubCommitDto> githubCommits = new List<GithubCommitDto>();

            var client = dto.Token == null
            ? new GitHubClient(new ProductHeaderValue("TeamSyncApp"))
            : new GitHubClient(new ProductHeaderValue("TeamSyncApp"))
            {
                Credentials = new Credentials(dto.Token)
            };

            var options = new ApiOptions
            {
                PageSize = 10,
                PageCount = 1,
                StartPage = 1
            };

            var commits = await client.Repository.Commit.GetAll(dto.Username, dto.RepositoryName, options);

            foreach (var commit in commits)
            {
                var resultCommit = new GithubCommitDto
                {
                    Author = commit.Commit.Author.Name,
                    Date = commit.Commit.Author.Date,
                    Message = commit.Commit.Message,
                };

                githubCommits.Add(resultCommit);
            }

            return githubCommits;
        }
    }
}
