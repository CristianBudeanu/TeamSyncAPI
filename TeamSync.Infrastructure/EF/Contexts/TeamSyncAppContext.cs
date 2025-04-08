using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TeamSync.Domain.Entities;
using TeamSync.Domain.Entities.GithubEntities;
using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Infrastructure.EF.Contexts
{
    public class TeamSyncAppContext : DbContext
    {
        public TeamSyncAppContext(DbContextOptions<TeamSyncAppContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<GithubRepository> GithubRepositories { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Domain.Entities.TaskEntities.TaskStatus> TaskStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
    }
}
