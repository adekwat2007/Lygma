using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Factories;

namespace GameTracker.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        private LibraryViewModelFactory _libraryViewModelFactory;
        private AddGameViewModelFactory _addGameViewModelFactory;

        [ObservableProperty] private object currentPage = new LibraryViewModel();

        public MainWindowViewModel()
        {
        }

        public MainWindowViewModel(LibraryViewModelFactory libraryViewModelFactory, AddGameViewModelFactory addGameViewModelFactory)
        {
            _libraryViewModelFactory = libraryViewModelFactory;
            _addGameViewModelFactory = addGameViewModelFactory;

            CurrentPage = _libraryViewModelFactory.CreateViewModel();
        }

        [RelayCommand]
        private void NavigateToLibrary(string page)
        {
            CurrentPage = _libraryViewModelFactory.CreateViewModel();
        }

        [RelayCommand]
        private void NavigateToAddGame(string page)
        {
            CurrentPage = _addGameViewModelFactory.CreateViewModel();
        }
    }
}