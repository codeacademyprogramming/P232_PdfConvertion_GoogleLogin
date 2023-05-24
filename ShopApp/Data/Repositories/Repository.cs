using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    { 
        private readonly ShopDbContext _context;

        public Repository(ShopDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp,params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return await query.FirstOrDefaultAsync(exp);
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return query.Where(exp);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return await query.AnyAsync(exp);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
