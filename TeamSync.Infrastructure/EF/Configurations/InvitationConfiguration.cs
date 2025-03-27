using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("Invitations");

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Project)
                .WithOne(i => i.Invitation)
                .HasForeignKey<Invitation>(fk => fk.ProjectId);
        }
    }
}
