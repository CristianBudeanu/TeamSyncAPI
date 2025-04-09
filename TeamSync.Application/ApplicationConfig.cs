using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Partner.Application.Common;
using TeamSync.Application.Services.Authentification;
using TeamSync.Application.Services.Chat;
using TeamSync.Application.Services.ProjectServices;
using TeamSync.Application.Services.ProjectServices.GithubServices;
using TeamSync.Application.Services.ProjectServices.InvitationServices;
using TeamSync.Application.Services.TaskServices;
using TeamSync.Helpers.HttpContextHelper;
using TeamSync.Helpers.FileHelper;
using TeamSync.Infrastructure;

namespace TeamSync.Application
{
    public static class ApplicationConfig
    {
        public static IServiceCollection ConfigAppLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<IGithubService, GithubService>();
            services.AddScoped<ITaskService, TaskService>();

            //Helper
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<IHttpContextService, HttpContextService>();

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingConfig).Assembly);
            var mapperConfig = new Mapper(config);
            services.AddSingleton<IMapper>(mapperConfig);

            services.ConfigInfrastructureLayer(configuration);

            return services;
        }
    }
}
