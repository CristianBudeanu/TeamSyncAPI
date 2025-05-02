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

        public async Task<bool> ValidateRepositoryCredentialsTask(GithubUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.RepositoryName) ||
                string.IsNullOrWhiteSpace(dto.Token))
                return false;
            
            var credentials = new Credentials(dto.Token); // Token auth
            var githubClient = new GitHubClient(new ProductHeaderValue("TeamSyncApp"))
            {
                Credentials = credentials
            };

            try
            {
                // Attempt to retrieve the repository
                var repository = await githubClient.Repository.Get(dto.Username, dto.RepositoryName);
                return repository != null;
            }
            catch (NotFoundException)
            {
                // Invalid repo name or insufficient permissions
                return false;
            }
            catch (AuthorizationException)
            {
                // Invalid token or unauthorized access
                return false;
            }
            catch (Exception)
            {
                // Optionally log the exception
                return false;
            }
        }
    }
}
