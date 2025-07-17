using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Enums;
using GameTracker.Factories.Interfaces;
using GameTracker.Models;
using GameTracker.Services;
using GameTracker.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace GameTracker.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject, IViewModel
    {
        public string PageName { get; set; } = "Main page";

        private RawgApiService _rawgService;
        private IViewModelFactory _viewModelFactory;

        [ObservableProperty] private object currentPage = new HomeViewModel();
        [ObservableProperty] private string currentPageName;

        public ObservableCollection<Game> SearchResults { get; } = new();

        public MainWindowViewModel()
        {
        }

        public MainWindowViewModel(RawgApiService rawgService, IViewModelFactory viewModelFactory)
        {
            _rawgService = rawgService;
            _viewModelFactory = viewModelFactory;
        }

        [RelayCommand]
        private void NavigateTo(string page)
        {
            switch (page)
            {
                case "Home":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Home);
                    CurrentPageName = ((HomeViewModel)CurrentPage).PageName;
                    break;

                case "Catalogue":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Catalogue);
                    CurrentPageName = ((CatalogueViewModel)CurrentPage).PageName;
                    break;

                case "Downloads":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Downloads);
                    CurrentPageName = ((DownloadsViewModel)CurrentPage).PageName;
                    break;

                case "Settings":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Settings);
                    CurrentPageName = ((SettingsViewModel)CurrentPage).PageName;
                    break;

                default:
                    throw new ArgumentException("ViewType has no ViewModel", nameof(page));
            }
        }

        [RelayCommand]
        private async Task SearchAsync(string query)
        {
            SearchResults.Clear();
            var results = await _rawgService.SearchGamesAsync(query);
            foreach (var game in results)
            {
                SearchResults.Add(game);
            }
        }
    }
}