using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    internal class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
    {
        public void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.ProjectRoleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(pr => pr.ProjectUserRoles)
                .WithOne(pur => pur.ProjectRole)
                .HasForeignKey(pur => pur.ProjectRoleId);

            builder.HasData
                    (
                    new List<ProjectRole>()
                    {
                    new ProjectRole
                    {
                        Id = Guid.NewGuid(),
                        ProjectRoleName = "Administrator"
                    },

                    new ProjectRole
                    {
                        Id = Guid.NewGuid(),
                        ProjectRoleName = "Member"
                    }
                    });
        }
    }
}
