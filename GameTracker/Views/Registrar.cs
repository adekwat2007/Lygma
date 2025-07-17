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

            services.AddTransient<HomePage>(s =>
            {
                var model = s.GetRequiredService<HomeViewModel>();
                var page = new HomePage()
                {
                    DataContext = model
                };

                return page;
            });

            services.AddTransient<CataloguePage>(s =>
            {
                var model = s.GetRequiredService<CatalogueViewModel>();
                var page = new CataloguePage()
                {
                    DataContext = model
                };

                return page;
            });

            return services;
        }
    }
}