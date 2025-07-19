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
        private CachingProvider _cachingProvider;
        private bool _isDataLoaded;

        [ObservableProperty] private IViewModel currentPage;

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

        public MainWindowViewModel(RawgApiService rawgService, IViewModelFactory viewModelFactory, CachingProvider cachingProvider)
        {
            _rawgService = rawgService;
            _viewModelFactory = viewModelFactory;
            _cachingProvider = cachingProvider;
        }

        private bool IsDataLoaded() => _isDataLoaded;

        [RelayCommand]
        private void NavigateToHome()
        {
            CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Home);
            CurrentPageName = ((HomeViewModel)CurrentPage).PageName;
            HomeSelected = true;
        }

        [RelayCommand(CanExecute = nameof(IsDataLoaded))]
        private void NavigateToCatalogue()
        {
            CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Catalogue);
            CurrentPageName = ((CatalogueViewModel)CurrentPage).PageName;
            CatalogueSelected = true;
        }

        [RelayCommand]
        private void NavigateToDownloads()
        {
            CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Downloads);
            CurrentPageName = ((DownloadsViewModel)CurrentPage).PageName;
            DownloadsSelected = true;
        }

        [RelayCommand]
        private void NavigateToSettings()
        {
            CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Settings);
            CurrentPageName = ((SettingsViewModel)CurrentPage).PageName;
            SettingsSelected = true;
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            NavigateToCatalogue();
            var model = (CatalogueViewModel)CurrentPage;

            model.SearchResults.Clear();

            var results = await _rawgService.SearchGamesAsync(searchQuery);
            foreach (var game in results)
            {
                model.SearchResults.Add(game);
            }
        }

        public async Task LoadData()
        {
            _cachingProvider.Genres = await _rawgService.GetGenresAsync();
            _cachingProvider.Platforms = await _rawgService.GetPlatformsAsync();
            _cachingProvider.Developers = await _rawgService.GetDevelopersAsync();

            _isDataLoaded = true;
            NavigateToCatalogueCommand.NotifyCanExecuteChanged();
        }
    }
}