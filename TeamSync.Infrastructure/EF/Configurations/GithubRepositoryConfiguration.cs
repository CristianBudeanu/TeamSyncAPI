using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities.GithubEntities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class GithubRepositoryConfiguration : IEntityTypeConfiguration<GithubRepository>
    {
        public void Configure(EntityTypeBuilder<GithubRepository> builder)
        {
            builder.HasKey(gr => gr.Id);

            builder.Property(gr => gr.RepositoryName).IsRequired();
            builder.Property(gr => gr.Username).IsRequired();

            builder.HasOne(gr => gr.Project)
                .WithOne(p => p.GithubRepository)
                .HasForeignKey<GithubRepository>(gr => gr.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
