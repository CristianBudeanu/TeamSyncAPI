namespace TeamSync.Application.Dto.GithubDtos
{
    public class GithubCommitDto
    {
        public string Author { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Message { get; set; }
    }
}
