using Mapster;
using TeamSync.Application.Dto.ProjectDtos;
using TeamSync.Application.Dto.TaskItemDtos;
using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Domain.Entities.TaskEntities;
using TeamSync.Domain.Enums;

namespace Partner.Application.Common
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProjectCreateDto, Project>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.Status, src => "Open")
                .Map(dest => dest.Completed, src => 0)
                .Map(dest => dest.CreationDate, src => DateTime.UtcNow);

            config.NewConfig<Project, ProjectDto>()
                .Map(dest => dest.Id, src => src.Id.ToString());
                //.Map(dest => dest.Image, src => src.Image);

            config.NewConfig<Project, ProjectPreviewDto>()
                .Map(dest => dest.Id, src => src.Id.ToString());

            config.NewConfig<TaskItemCreateDto, TaskItem>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.CreatedDate, src => DateTime.UtcNow)
                .Map(dest => dest.Priority, src => Enum.Parse<TaskPriority>(src.Priority, true));

            config.NewConfig<TaskItem, TaskItemDto>()
                .Map(dest => dest.Status, src => src.Status.StatusName)
                .Map(dest => dest.ProjectName, src => src.Project.Name)
                .Map(dest => dest.ProjectImage, src => src.Project.Image);
        }
    }
}
