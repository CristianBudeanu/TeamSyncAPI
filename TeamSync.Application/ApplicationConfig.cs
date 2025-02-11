using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamSync.Application.Services.Authentification;
using TeamSync.Application.Services.Chat;
using TeamSync.Application.Services.Project;
using TeamSync.Infrastructure;

namespace TeamSync.Application
{
    public static class ApplicationConfig
    {
        public static IServiceCollection ConfigAppLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddSingleton<ChatService>();

            services.ConfigInfrastructureLayer(configuration);

            return services;
        }
    }
}
