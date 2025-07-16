using GameTracker.Models.Interfaces;

namespace GameTracker.Services.Interfaces
{
    internal interface IRepository<T> where T : IEntity
    {
        Task AddAsync(T entity);

        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}