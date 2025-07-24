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

        private IViewModelFactory _viewModelFactory;
        private bool _isDataLoaded;

        [ObservableProperty] private IViewModel currentPage = new CatalogueViewModel();

        [ObservableProperty] private string currentPageName;

        [ObservableProperty] private bool homeSelected;
        [ObservableProperty] private bool catalogueSelected;
        [ObservableProperty] private bool downloadsSelected;
        [ObservableProperty] private bool settingsSelected;

        [ObservableProperty] private bool splitterPressed;

        public MainWindowViewModel()
        {
        }

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        private bool IsDataLoaded() => _isDataLoaded;

        [RelayCommand]
        private void NavigateToHome()
        {
            CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Home);
            CurrentPageName = ((HomeViewModel)CurrentPage).PageName;
            HomeSelected = true;
        }

        [RelayCommand]
        private async Task NavigateToCatalogue()
        {
            CurrentPage = _viewModelFactory.CreateViewModel(ViewType.Catalogue);
            CurrentPageName = ((CatalogueViewModel)CurrentPage).PageName;
            CatalogueSelected = true;

            await ((CatalogueViewModel)CurrentPage).LoadData();
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
        }
    }
}