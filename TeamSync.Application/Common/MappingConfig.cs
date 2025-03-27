using Mapster;
using TeamSync.Application.Dto.ProjectDtos;
using TeamSync.Application.Dto.TaskItemDtos;
using TeamSync.Domain.Entities.ProjectEntities;
using TeamSync.Domain.Entities.TaskEntities;

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

            config.NewConfig<Project, ProjectPreviewDto>()
                .Map(dest => dest.Id, src => src.Id.ToString());

            config.NewConfig<TaskItemCreateDto, TaskItem>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.CreatedDate, src => DateTime.UtcNow);

            config.NewConfig<TaskItem, TaskItemDto>()
                .Map(dest => dest.Status, src => src.Status.StatusName);
        }
    }
}
