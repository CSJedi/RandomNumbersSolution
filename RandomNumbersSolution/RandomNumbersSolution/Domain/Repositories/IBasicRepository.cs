using System.Threading.Tasks;

namespace RandomNumbersSolution.Domain.Repositories
{
    public interface IBasicRepository<TKey, TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetAsync(TKey key);
        Task<TEntity> TryGetAsync(TKey key);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TKey key);
    }
}
