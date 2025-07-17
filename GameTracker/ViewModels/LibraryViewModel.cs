using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Enums;
using GameTracker.Models;
using GameTracker.Services.Interfaces;
using System.Collections.ObjectModel;
using GameTracker.Factories;

namespace GameTracker.ViewModels
{
    internal partial class LibraryViewModel : ObservableObject
    {
        private IRepository<Game> _gameRepository;

        public ObservableCollection<Game> Games { get; set; } = new();

        public LibraryViewModel()
        {
            for (int i = 1; i <= 5; i++)
            {
                Games.Add(new Game()
                {
                    Id = i,
                    Name = $"Game {i}",
                    CompletionStatus = CompletionStatus.Completed,
                    Genres = new List<Genre>() { Genre.Action },
                    Platforms = new List<Platform>() { Platform.PC },
                    PersonalRating = 5,
                    ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                    GameCoverPath = "test"
                });
            }
        }

        public LibraryViewModel(IRepository<Game> gameRepository)
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
                        Genres = new List<Genre>() { Genre.Action },
                        Platforms = new List<Platform>() { Platform.PC },
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
                        Genres = new List<Genre>() { Genre.Action },
                        Platforms = new List<Platform>() { Platform.PC },
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
        private async Task AddTestGames() => await AddTestGames(5);

        [RelayCommand]
        private async Task DeleteAllGames()
        {
            await _gameRepository.DeleteAllAsync();
            await LoadGamesAsync();
        }
    }
}