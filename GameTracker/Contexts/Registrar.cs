using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Contexts
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<GameDbContext>();

            return services;
        }
    }
}