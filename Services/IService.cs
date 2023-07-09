using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IService<T>
    {
        IQueryable<T> GetAll();
        Task<T?> GetOneAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        bool DoesExist(int id);
    }
}
