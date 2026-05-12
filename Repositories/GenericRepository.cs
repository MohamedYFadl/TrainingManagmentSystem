
using Microsoft.EntityFrameworkCore;
using MVC02.Interfaces;
using MVC02.Models;
using MVC02.Specifications;

namespace MVC02.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ITIDBContext _context;
        public GenericRepository(ITIDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
             return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specs)
        {
            return await ApplySpecs(specs).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(ISpecification<T> specs)
        {
            return await ApplySpecs(specs).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> specs)
        {
            return await ApplySpecs(specs).CountAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        private IQueryable<T> ApplySpecs(ISpecification<T> specs)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specs);
        }
    }
}
