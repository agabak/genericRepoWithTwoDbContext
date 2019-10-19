using Identity.Data.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Identity.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetSingle(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {  // need more prove on this method
                  _dbSet.Attach(entity);
                  _context.Entry(entity).State = EntityState.Modified;
           await  _context.SaveChangesAsync().ConfigureAwait(false);
            return entity; 
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // still have performance question about this methed
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
