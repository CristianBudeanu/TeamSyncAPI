using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities.ChatEntities;
using TeamSync.Domain.Entities.GithubEntities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
            .HasOne(m => m.Project)
            .WithMany(p => p.chatMessages)
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.FromUser)
                .WithMany()
                .HasForeignKey(m => m.FromId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
