using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service <T>: IService<T> where T : class
    {
        protected AppDbContext _context;



        public Service(AppDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        //***NOTE: Return type is Nullable.***//
        public virtual async Task<T?> GetOneAsync(int id)
        {
            if (GetAll() is DbSet<T> all)
                return await all.FindAsync(id);
            else
                return await Task.FromResult<T?>(null);

        }
        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public virtual async Task<bool> EditAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
        public virtual async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public virtual bool DoesExist(int id)
        {
            return GetOneAsync(id) != null;
        }

    }
}
