using CommunityToolkit.Mvvm.ComponentModel;
using GameTracker.Models;
using GameTracker.Services;
using GameTracker.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace GameTracker.ViewModels
{
    internal partial class CatalogueViewModel : ObservableObject, IViewModel
    {
        private CachingProvider _cachingProvider;

        [ObservableProperty] private GameResponse gameResponse;

        public string PageName { get; set; } = "Catalogue";

        public ObservableCollection<Genre> Genres { get; set; } = new();
        public ObservableCollection<Platform> Platforms { get; set; } = new();
        public ObservableCollection<Developer> Developers { get; set; } = new();
        public ObservableCollection<Game> Games { get; set; } = new();

        public CatalogueViewModel()
        {
        }

        public CatalogueViewModel(CachingProvider cachingProvider)
        {
            _cachingProvider = cachingProvider;

            LoadCache();
        }

        private void LoadCache()
        {
            if (_cachingProvider.Genres is not null)
                Genres = new ObservableCollection<Genre>(_cachingProvider.Genres);
            if (_cachingProvider.Platforms is not null)
                Platforms = new ObservableCollection<Platform>(_cachingProvider.Platforms);
            if (_cachingProvider.Developers is not null)
                Developers = new ObservableCollection<Developer>(_cachingProvider.Developers);
            if (_cachingProvider.Games is not null)
            {
                GameResponse = _cachingProvider.Games;
                Games = new ObservableCollection<Game>(GameResponse.Results);
            }
        }
    }
}