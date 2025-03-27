using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskStatus = TeamSync.Domain.Entities.TaskEntities.TaskStatus;

namespace TeamSync.Infrastructure.EF.Configurations
{
    public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatus>
    {
        public void Configure(EntityTypeBuilder<TaskStatus> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.StatusName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(ts => ts.TaskItems)
                .WithOne(ti => ti.Status)
                .HasForeignKey(ti => ti.StatusId);


            builder.HasData
               (
               new List<TaskStatus>()
               {
                    new TaskStatus
                    {
                        Id = Guid.NewGuid(),
                        StatusName = "Pending"
                    },

                    new TaskStatus
                    {
                        Id = Guid.NewGuid(),
                        StatusName = "InWork"
                    },

                    new TaskStatus
                    {
                        Id = Guid.NewGuid(),
                        StatusName = "Done"
                    },

                    new TaskStatus
                    {
                        Id = Guid.NewGuid(),
                        StatusName = "Closed"
                    }


               });
        }
    }
}
