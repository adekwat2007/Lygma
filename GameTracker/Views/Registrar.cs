using GameTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Views
{
    internal static class Registrar
    {
        public static IServiceCollection RegisterViews(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>(s =>
            {
                var model = s.GetRequiredService<MainWindowViewModel>();
                var window = new MainWindow()
                {
                    DataContext = model
                };

                return window;
            });

            return services;
        }
    }
}