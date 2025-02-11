using System.ComponentModel.DataAnnotations;

namespace TeamSync.Domain.Entities.ProjectEntities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Completed { get; set; }
        public string GitHubUrl { get; set; }
        public DateTime Created { get; set; }
    }
}
