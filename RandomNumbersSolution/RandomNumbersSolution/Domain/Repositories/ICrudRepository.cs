using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumbersSolution.Domain.Repositories
{
    public interface ICrudRepository<TKey, TEntity> : IBasicRepository<TKey, TEntity> where TEntity : class, new()
    {
        Task<IList<TEntity>> GetAllAsync();
        Task UpdateAsync<TProperty>(TKey key, Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);
        Task UpdateAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveAsync(TEntity entity);
    }
}
