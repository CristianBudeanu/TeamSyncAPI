using System.ComponentModel.DataAnnotations;

namespace TeamSync.Domain.Entities.TeamEntities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}
