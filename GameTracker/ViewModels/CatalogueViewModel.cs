using CommunityToolkit.Mvvm.ComponentModel;
using GameTracker.Models;
using GameTracker.Services;
using GameTracker.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace GameTracker.ViewModels
{
    internal partial class CatalogueViewModel : ObservableObject, IViewModel
    {
        private RawgApiService _rawgService;

        public string PageName { get; set; } = "Catalogue";

        public ObservableCollection<Game> SearchResults { get; } = new();
        public ObservableCollection<Genre> Genres { get; set; } = new();
        public ObservableCollection<Platform> Platforms { get; set; } = new();
        public ObservableCollection<Developer> Developers { get; set; } = new();

        public CatalogueViewModel()
        {
            Genres = new ObservableCollection<Genre>(Enumerable.Range(1, 20).Select(i => new Genre()
            {
                Name = "Test"
            }));
        }

        public CatalogueViewModel(RawgApiService rawgService)
        {
            _rawgService = rawgService;
            LoadGenresAsync();
        }

        public async Task LoadGenresAsync()
        {
            Genres.Clear();

            var genres = await _rawgService.GetGenresAsync(100);

            foreach (var genre in genres)
            {
                Genres.Add(genre);
            }
        }
    }
}