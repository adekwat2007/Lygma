using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Enums;
using GameTracker.Factories.Interfaces;
using GameTracker.Models;
using GameTracker.Services;
using GameTracker.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GameTracker.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject, IViewModel
    {
        public string PageName { get; set; } = "Main page";

        private RawgApiService _rawgService;
        private IViewModelFactory _viewModelFactory;

        [ObservableProperty] private IViewModel currentPage = new CatalogueViewModel();
        [ObservableProperty] private string currentPageName;
        [ObservableProperty] private string searchQuery;

        [ObservableProperty] private bool homeSelected;
        [ObservableProperty] private bool catalogueSelected;
        [ObservableProperty] private bool downloadsSelected;
        [ObservableProperty] private bool settingsSelected;

        [ObservableProperty] private bool splitterPressed;

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
                    HomeSelected = true;
                    break;

                case "Catalogue":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Catalogue);
                    CurrentPageName = ((CatalogueViewModel)CurrentPage).PageName;
                    CatalogueSelected = true;
                    break;

                case "Downloads":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Downloads);
                    CurrentPageName = ((DownloadsViewModel)CurrentPage).PageName;
                    DownloadsSelected = true;
                    break;

                case "Settings":
                    CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Settings);
                    CurrentPageName = ((SettingsViewModel)CurrentPage).PageName;
                    SettingsSelected = true;
                    break;

                default:
                    throw new ArgumentException("ViewType has no ViewModel", nameof(page));
            }
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            NavigateTo("Catalogue");
            var model = (CatalogueViewModel)CurrentPage;

            model.SearchResults.Clear();

            var results = await _rawgService.SearchGamesAsync(searchQuery);
            foreach (var game in results)
            {
                model.SearchResults.Add(game);
            }
        }
    }
}