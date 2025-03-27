using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Infrastructure
{
    public static class InfrastructureConfig
    {
        public static IServiceCollection ConfigInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("connstringSqlServer");

            services.AddDbContext<TeamSyncAppContext>(options =>
              options.UseSqlServer(connectionString));

            return services;
        }
    }
}
