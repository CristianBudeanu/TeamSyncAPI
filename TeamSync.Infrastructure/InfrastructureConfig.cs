using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamSync.Infrastructure.EF.Contexts;
using TeamSync.Infrastructure.EF.Repositories;
using TeamSync.Infrastructure.EF.Repositories.Project;

namespace TeamSync.Infrastructure
{
    public static class InfrastructureConfig
    {
        public static IServiceCollection ConfigInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("connstringSqlServer");

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<UserContext>(options =>
              options.UseSqlServer(connectionString));

            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddDbContext<ProjectContext>(options =>
              options.UseSqlServer(connectionString));


            //services.AddDbContext<UserContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("connstringPostgreDb")));

            //services.AddDbContext<TeamContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("connstringPostgreDb")));

            //EnsureDatabaseMigrated<UserContext>(services);
            //EnsureDatabaseMigrated<TeamContext>(services);

            return services;
        }

        //private static void EnsureDatabaseMigrated<TContext>(IServiceCollection services) where TContext : DbContext
        //{
        //    var serviceProvider = services.BuildServiceProvider();

        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        //        dbContext.Database.Migrate();
        //    }
        //}
    }
}
