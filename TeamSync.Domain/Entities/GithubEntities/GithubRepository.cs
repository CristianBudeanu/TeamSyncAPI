using System.ComponentModel.DataAnnotations;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Domain.Entities.GithubEntities
{
    public class GithubRepository
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string RepositoryName { get; set; }
        public string? Token { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
