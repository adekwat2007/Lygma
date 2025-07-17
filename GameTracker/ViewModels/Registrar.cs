using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.ViewModels
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddTransient<HomeViewModel>();
            services.AddTransient<CatalogueViewModel>();
            services.AddTransient<DownloadsViewModel>();
            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<SettingsViewModel>();

            return services;
        }
    }
}