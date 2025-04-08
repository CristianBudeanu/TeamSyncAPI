using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamSync.Domain.Entities;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            builder.HasData(
                new List<Role>()
                {
                    new Role
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        RoleName = "User"
                    },

                    new Role
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        RoleName = "Admin"
                    }
                });


        }
    }
}
