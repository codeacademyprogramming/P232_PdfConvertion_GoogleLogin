using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRepository<TEntity>
    {
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp,params string[] includes);
        public Task<List<TEntity>> GetAllAsync(params string[] includes);
        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity,bool>> exp, params string[] includes);
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        public Task AddAsync(TEntity brand);
        public void Remove(TEntity brand);
        public Task SaveChangesAsync();
    }
}
