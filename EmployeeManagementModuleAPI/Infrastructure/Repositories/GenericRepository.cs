using EmployeeManagementModuleAPI.Core.Interfaces;
using EmployeeManagementModuleAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementModuleAPI.Infrastructure.Repositories
{
    // Infrastructure/Repositories/GenericRepository.cs
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}

        //public async Task<T> GetByIdAsync(object id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        //public async Task AddAsync(T entity)
        //{
        //    await _dbSet.AddAsync(entity);
        //}

        //public void Update(T entity)
        //{
        //    _dbSet.Update(entity);
        //}

        //public void Delete(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}

        //public async Task SaveAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }

}
