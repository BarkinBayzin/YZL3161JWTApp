using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JWTAppBackOffice.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly JWTContext _jwtContext;
        public Repository(JWTContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _jwtContext.Set<T>().AddAsync(entity);
            await _jwtContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync() => 
            await _jwtContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter) =>
            await _jwtContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);

        public async Task<T> GetByIdAsync(object id) => 
            await _jwtContext.Set<T>().FindAsync(id);

        public async Task RemoveAsync(T entity)
        {
            _jwtContext.Set<T>().Remove(entity);
            await _jwtContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _jwtContext.Set<T>().Update(entity);
            await _jwtContext.SaveChangesAsync();
        }
    }
}
