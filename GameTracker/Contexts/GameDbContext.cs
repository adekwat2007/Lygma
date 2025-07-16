using GameTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Contexts
{
    internal class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }
    }
}