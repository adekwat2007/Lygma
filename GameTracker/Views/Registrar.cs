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

            services.AddTransient<LibraryPage>(s =>
            {
                var model = s.GetRequiredService<LibraryViewModel>();
                var page = new LibraryPage()
                {
                    DataContext = model
                };

                return page;
            });

            services.AddTransient<AddGamePage>(s =>
            {
                var model = s.GetRequiredService<AddGameViewModel>();
                var page = new AddGamePage()
                {
                    DataContext = model
                };

                return page;
            });

            return services;
        }
    }
}