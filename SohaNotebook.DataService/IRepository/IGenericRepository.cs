using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SohaNotebook.DataService.IRepository
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<bool> Add(T entitiy);

        Task<bool> Delete(Guid id);

        Task<bool> Upsert(T entity);
        
    }
}