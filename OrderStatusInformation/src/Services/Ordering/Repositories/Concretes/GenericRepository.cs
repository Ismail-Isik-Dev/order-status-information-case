using Microsoft.EntityFrameworkCore;
using Ordering.Comman;
using Ordering.Data;
using Ordering.Repositories.Contracts;
using System.Linq.Expressions;

namespace Ordering.Repositories.Concretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        private readonly OrderDbContext _context;

        public GenericRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> CreateRangeAsync(List<T> entities)
        {
            await _context.Set<List<T>>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _context.Set<T>().Remove(entity); });
            await _context.SaveChangesAsync();

        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includedProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includedProperties.Any())
            {
                foreach (var includedProperty in includedProperties)
                {
                    query = query.Include(includedProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includedProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includedProperties.Any())
            {
                foreach (var includedPropertie in includedProperties)
                {
                    query = query.Include(includedPropertie);
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => { _context.Set<T>().Update(entity); });
            await _context.SaveChangesAsync();
        }
    }
}
