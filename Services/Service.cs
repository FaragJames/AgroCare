using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service <T> where T : class
    {
        AppDbContext _context;


        public Service(AppDbContext context)
        {
            _context = context;
        }
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<T?> GetOne(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<bool> Add(T entity)
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
        public async Task<bool> Edit(T entity)
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
        public async Task<bool> Remove(int id)
        {
            try
            {
                _context.Remove(GetOne(id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DoesExist(int id)
        {
            return GetOne(id) != null;
        }

    }
}
