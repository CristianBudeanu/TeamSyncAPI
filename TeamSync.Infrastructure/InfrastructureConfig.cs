using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamSync.Infrastructure.EF.Contexts;
using TeamSync.Infrastructure.EF.Repositories;

namespace TeamSync.Infrastructure
{
    public static class InfrastructureConfig
    {
        public static IServiceCollection ConfigInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("connstringSqlServer")));


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
