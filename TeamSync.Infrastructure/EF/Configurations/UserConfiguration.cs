using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.PassHash)
                .IsRequired();

            builder.Property(u => u.PassSalt)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            builder.HasMany(u => u.Projects)
                .WithMany(p => p.Members);

            builder.HasMany(u => u.ProjectUserRoles)
                .WithOne(pur => pur.User)
                .HasForeignKey(pur => pur.UserId);

            builder.HasMany(u => u.AssignedTasks)
                .WithOne(t => t.AssignedUser)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Username = "admin",
                RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Email = "cristian.budeanu02@gmail.com",
                PassHash = "bLJvnAQ9vt72eCQkkRpdj6Cok6kRjqdJH1GYR19FjnU=",
                PassSalt = "C4F05POfmvkLjjolBSCiGw=="
            });
        }
    }
}
