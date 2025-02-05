using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamSync.Application.Services.Authentification;
using TeamSync.Application.Services.Chat;
using TeamSync.Infrastructure;

namespace TeamSync.Application
{
    public static class ApplicationConfig
    {
        public static IServiceCollection ConfigAppLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<ChatService>();
            services.AddSignalR();

            services.ConfigInfrastructureLayer(configuration);

            return services;
        }
    }
}
