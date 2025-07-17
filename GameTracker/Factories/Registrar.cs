using GameTracker.Factories.Interfaces;
using GameTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Factories
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            return services;
        }
    }
}