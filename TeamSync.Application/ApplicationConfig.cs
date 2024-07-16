using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSync.Application.Services.Authentification;

namespace TeamSync.Application
{
    public static class ApplicationConfig
    {
        public static IServiceCollection ConfigAppLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
