using GameTracker.Enums;
using GameTracker.Factories.Interfaces;
using GameTracker.ViewModels;
using GameTracker.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Factories
{
    internal class ViewModelFactory : IViewModelFactory
    {
        private IServiceProvider _services;

        public ViewModelFactory(IServiceProvider services)
        {
            _services = services;
        }

        public IViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _services.GetRequiredService<HomeViewModel>();

                case ViewType.Catalogue:
                    return _services.GetRequiredService<CatalogueViewModel>();

                case ViewType.Downloads:
                    return _services.GetRequiredService<DownloadsViewModel>();

                case ViewType.Settings:
                    return _services.GetRequiredService<SettingsViewModel>();

                default:
                    throw new ArgumentException("ViewType has no ViewModel", nameof(viewType));
            }
        }
    }
}