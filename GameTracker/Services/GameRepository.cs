using GameTracker.Contexts;
using GameTracker.Models;
using GameTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Services
{
    internal class GameRepository : IRepository<Game>
    {
        private readonly GameDbContext _context;
        private readonly DbSet<Game> _dbSet;

        public GameRepository(GameDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Game>();
        }

        public async Task AddAsync(Game entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is not null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(Game entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}