using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SohaNotebook.DataService.Data;
using SohaNotebook.DataService.IRepository;

namespace SohaNotebook.DataService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(AppDbContext context,ILogger logger)
        {
            _context = context;
            dbSet = _context.Set<T>();
            _logger=logger;

        }
        public async Task<bool> Add(T entitiy)
        {
            await dbSet.AddAsync(entitiy);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> Get(Guid id)
        {
            var result=await dbSet.FindAsync(id);
            return result;
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
           return await dbSet.ToListAsync();
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}