using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Enums;
using GameTracker.Models;
using GameTracker.Services.Interfaces;
using System.Collections.ObjectModel;

namespace GameTracker.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        private IRepository<Game> _gameRepository;

        public ObservableCollection<Game> Games { get; set; } = new();

        public MainWindowViewModel()
        {
            // Для DesignInstance

            for (int i = 0; i < 10; i++)
            {
                Games.Add(new Game()
                {
                    Id = i + 1,
                    Name = $"Game {i}",
                    CompletionStatus = CompletionStatus.Completed,
                    Genre = Genre.Action,
                    PersonalRating = 5,
                    ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                    GameCoverPath = "test"
                });
            }
        }

        public MainWindowViewModel(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        private async Task AddTestGames(int n)
        {
            var lastGame = _gameRepository.GetAllAsync().Result.LastOrDefault();

            if (lastGame is not null)
            {
                for (int i = 1; i <= n; i++)
                {
                    await _gameRepository.AddAsync(new Game()
                    {
                        Id = i + lastGame.Id,
                        Name = $"Game {i}",
                        CompletionStatus = CompletionStatus.Completed,
                        Genre = Genre.Action,
                        PersonalRating = 5,
                        ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                        GameCoverPath = "test"
                    });
                }
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    await _gameRepository.AddAsync(new Game()
                    {
                        Id = i,
                        Name = $"Game {i}",
                        CompletionStatus = CompletionStatus.Completed,
                        Genre = Genre.Action,
                        PersonalRating = 5,
                        ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                        GameCoverPath = "test"
                    });
                }
            }

            await LoadGamesAsync();
        }

        public async Task LoadGamesAsync()
        {
            Games.Clear();

            var games = await _gameRepository.GetAllAsync();
            foreach (var game in games)
            {
                Games.Add(game);
            }
        }

        [RelayCommand]
        private async Task LoadGames() => await LoadGamesAsync();

        [RelayCommand]
        private async Task DeleteGame(object p)
        {
            if (p is Game game)
            {
                await _gameRepository.DeleteAsync(game.Id);
                await LoadGamesAsync();
            }
        }

        [RelayCommand]
        private async Task AddGames() => await AddTestGames(5);
    }
}