﻿using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Auxiliary;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;

namespace Services
{
    public class Service<T>: IService<T> where T : class, IBaseProperties
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
            var result = GetAll();
            if (result is DbSet<T> all)
                return await all.FindAsync(id);
            else
            {
                //var primaryKey = typeof(T).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);
                //return result.AsEnumerable().FirstOrDefault(e => primaryKey!.GetValue(e)!.Equals(id));
                return await result.FirstOrDefaultAsync(e => e.Id == id);
            }
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
        public virtual async Task<bool> DoesExist(int id)
        {
            return await GetOneAsync(id) != null;
        }
    }
}