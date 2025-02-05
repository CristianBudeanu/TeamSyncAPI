using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Infrastructure.EF.Contexts
{
    public class UserContext(IConfiguration configuration) : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("connstringSqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add additional configurations if needed
            base.OnModelCreating(modelBuilder);
        }
    } 
}
