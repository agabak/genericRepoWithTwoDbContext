using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Identity.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetSingle(object id);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Create(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
