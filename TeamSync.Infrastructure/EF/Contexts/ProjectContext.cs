using Microsoft.EntityFrameworkCore;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Infrastructure.EF.Contexts
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add additional configurations if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
