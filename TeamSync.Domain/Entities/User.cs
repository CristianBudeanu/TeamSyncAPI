using System.ComponentModel.DataAnnotations;
using TeamSync.Domain.Enums;

namespace TeamSync.Domain.Entities.TaskEntities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public Roles Role {  get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public string PassSalt { get; set; }
    }
}
