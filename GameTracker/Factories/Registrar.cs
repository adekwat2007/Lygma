using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Factories
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            services.AddSingleton<LibraryViewModelFactory>();
            services.AddSingleton<AddGameViewModelFactory>();

            return services;
        }
    }
}
