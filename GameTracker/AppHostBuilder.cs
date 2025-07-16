using GameTracker.Contexts;
using GameTracker.Services;
using GameTracker.ViewModels;
using GameTracker.Views;
using Microsoft.Extensions.Hosting;

namespace GameTracker
{
    internal static class AppHostBuilder
    {
        private static readonly string dbPath = @"Data\games.db";

        public static IHost Build()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.RegisterServices()
                        .RegisterViewModels()
                        .RegisterViews()
                        .RegisterDbContexts();
                })
                .Build();
        }
    }
}