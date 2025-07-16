using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Contexts
{
    internal static class Registrar
    {
        private static readonly string dbPath = @"Data\games.db";

        public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<GameDbContext>(options =>
                options.UseSqlite($"Data Source = {dbPath}"));

            return services;
        }
    }

    internal class GameDbContextFactory : IDesignTimeDbContextFactory<GameDbContext>
    {
        private static readonly string dbPath = @"Data\games.db";

        public GameDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameDbContext>();
            optionsBuilder.UseSqlite($"Data Source = {dbPath}");

            return new GameDbContext(optionsBuilder.Options);
        }
    }
}