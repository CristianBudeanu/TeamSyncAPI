using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class ProjectUserRoleConfiguration : IEntityTypeConfiguration<ProjectUserRole>
    {
        public void Configure(EntityTypeBuilder<ProjectUserRole> builder)
        {
            builder.HasKey(pur => new { pur.UserId, pur.ProjectId, pur.ProjectRoleId });

            builder.Property(pur => pur.AssignedAt)
                .IsRequired();

            builder.HasOne(pur => pur.User)
                .WithMany(u => u.ProjectUserRoles)
                .HasForeignKey(pur => pur.UserId);

            builder.HasOne(pur => pur.Project)
                .WithMany(p => p.ProjectUserRoles)
                .HasForeignKey(pur => pur.ProjectId);

            builder.HasOne(pur => pur.ProjectRole)
                .WithMany(pr => pr.ProjectUserRoles)
                .HasForeignKey(pur => pur.ProjectRoleId);
        }
    }
}
