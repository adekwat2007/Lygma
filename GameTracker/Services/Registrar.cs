using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Services
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<RawgApiService>();

            return services;
        }
    }
}