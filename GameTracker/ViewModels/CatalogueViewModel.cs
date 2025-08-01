﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameTracker.Models;
using GameTracker.Services;
using GameTracker.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace GameTracker.ViewModels
{
    internal partial class CatalogueViewModel : ObservableObject, IViewModel
    {
        private RawgApiService _rawgApiService;
        private CachingProvider _cachingProvider;
        public string PageName { get; set; } = "Catalogue";

        [ObservableProperty] private GameResponse gameResponse;

        [ObservableProperty] private bool genresLoaded;
        [ObservableProperty] private bool platformsLoaded;
        [ObservableProperty] private bool developersLoaded;
        [ObservableProperty] private bool gamesLoaded;
        [ObservableProperty] private bool isFirstPageButtonVisible;

        [ObservableProperty] private int currentPage = 1;
        [ObservableProperty] private int maxPage;
        [ObservableProperty] private int gamesCount;

        public ObservableCollection<Genre> Genres { get; set; } = new();
        public ObservableCollection<Platform> Platforms { get; set; } = new();
        public ObservableCollection<Developer> Developers { get; set; } = new();
        public ObservableCollection<Game> Games { get; set; } = new(new Game[20]);
        public ObservableCollection<int> Pages { get; set; } = new() { 1, 2, 3 };

        public CatalogueViewModel()
        {
            Games.Add(new Game()
            {
                Background_Image = "https://media.rawg.io/media/games/20a/20aa03a10cda45239fe22d035c0ebe64.jpg",
                Name = "Grand Theft Auto V",
                Genres = new List<Genre>()
                {
                    new Genre(){Name = "Action"}
                },
                Platforms = new List<GamePlatformResponse>()
                {
                    new GamePlatformResponse(){Platform = new Platform(){Name = "Pc"}},
                    new GamePlatformResponse(){Platform = new Platform(){Name = "PlayStation 5"}}
                }
            });
        }

        public CatalogueViewModel(RawgApiService rawgApiService, CachingProvider cachingProvider)
        {
            _rawgApiService = rawgApiService;
            _cachingProvider = cachingProvider;
        }

        public async Task GetMaxPage()
        {
            var response = await _rawgApiService.GetGamesCountAsync();
            GamesCount = response.Count;
            MaxPage = GamesCount % 20 == 0 ? GamesCount / 20 : GamesCount / 20 + 1;
        }

        public async Task LoadData()
        {
            if (!GenresLoaded)
            {
                Genres.Clear();

                if (_cachingProvider.Genres is null)
                {
                    var genres = await _rawgApiService.GetGenresAsync();
                    _cachingProvider.Genres = genres;
                    foreach (var genre in genres)
                        Genres.Add(genre);
                }
                else
                {
                    foreach (var cachingProviderGenre in _cachingProvider.Genres)
                    {
                        Genres.Add(cachingProviderGenre);
                    }
                }

                GenresLoaded = true;
            }

            if (!PlatformsLoaded)
            {
                Platforms.Clear();

                if (_cachingProvider.Platforms is null)
                {
                    var platforms = await _rawgApiService.GetPlatformsAsync();
                    _cachingProvider.Platforms = platforms;
                    foreach (var platform in platforms)
                        Platforms.Add(platform);
                }
                else
                {
                    foreach (var cachingProviderPlatform in _cachingProvider.Platforms)
                    {
                        Platforms.Add(cachingProviderPlatform);
                    }
                }

                PlatformsLoaded = true;
            }

            if (!DevelopersLoaded)
            {
                Developers.Clear();

                if (_cachingProvider.Developers is null)
                {
                    var developers = await _rawgApiService.GetDevelopersAsync();
                    _cachingProvider.Developers = developers;
                    foreach (var developer in developers)
                        Developers.Add(developer);
                }
                else
                {
                    foreach (var cachingProviderDeveloper in _cachingProvider.Developers)
                    {
                        Developers.Add(cachingProviderDeveloper);
                    }
                }

                DevelopersLoaded = true;
            }

            if (!GamesLoaded)
            {
                Games.Clear();

                if (_cachingProvider.Games is null)
                {
                    var games = await _rawgApiService.GetGamesPageAsync(1);
                    _cachingProvider.Games = games;
                    foreach (var game in games.Results)
                    {
                        Games.Add(game);
                    }
                }
                else
                {
                    foreach (var cachingProviderGame in _cachingProvider.Games.Results)
                    {
                        Games.Add(cachingProviderGame);
                    }
                }

                GamesLoaded = true;
            }
        }

        private async Task LoadGamePage(int page)
        {
            GamesLoaded = false;
            Games.Clear();

            var games = await _rawgApiService.GetGamesPageAsync(page);
            _cachingProvider.Games = games;
            foreach (var game in games.Results)
                Games.Add(game);

            GamesLoaded = true;
        }

        [RelayCommand]
        private async Task ChangeGamePage(int page)
        {
            if (page == Pages[2])
            {
                Pages[0]++;
                Pages[1]++;
                Pages[2]++;
            }
            if (CurrentPage > 2 && page == Pages[0])
            {
                Pages[0] = page - 1;
                Pages[1] = page;
                Pages[2] = page + 1;
            }

            IsFirstPageButtonVisible = page > 2;


            CurrentPage = page;

            await LoadGamePage(page);
        }
    }
}