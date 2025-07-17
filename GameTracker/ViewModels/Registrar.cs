using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.ViewModels
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddTransient<LibraryViewModel>();
            services.AddTransient<AddGameViewModel>();
            services.AddTransient<MainWindowViewModel>();

            return services;
        }
    }
}