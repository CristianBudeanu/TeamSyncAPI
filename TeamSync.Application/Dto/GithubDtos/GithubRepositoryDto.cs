namespace TeamSync.Application.Dto.GithubDtos
{
    public class GithubRepositoryDto
    {
        public string Username { get; set; }
        public string RepositoryName { get; set; }
        public string? Token { get; set; }
        public List<GithubCommitDto>? GithubCommits { get; set; }
    }
}
