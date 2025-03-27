using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities;
using TeamSync.Domain.Entities.GithubEntities;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Category)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.Completed)
                .IsRequired();

            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.HasMany(p => p.Members)
                .WithMany(u => u.Projects);

            builder.HasMany(p => p.ProjectUserRoles)
                .WithOne(pur => pur.Project)
                .HasForeignKey(pur => pur.ProjectId);

            //builder.HasOne(p => p.Invitation)
            //    .WithOne()
            //    .HasForeignKey<Invitation>(i => i.ProjectId)
            //     .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(p => p.GithubRepository)
            //    .WithOne();

            builder.HasMany(p => p.TaskItems)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}
