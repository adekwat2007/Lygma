using GameTracker.Models;
using GameTracker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Services
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Game>, GameRepository>();

            return services;
        }
    }
}