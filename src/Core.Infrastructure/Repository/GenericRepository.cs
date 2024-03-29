﻿using Core.Application.IRepositories;
using Core.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

// The Repository and Unit of Work Patterns
// https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

namespace Core.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _db.CountAsync();
            }
            return await _db.CountAsync(expression);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _db.AnyAsync(expression);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _db.FindAsync(id);
            if (record != null)
            {
                _db.Remove(record);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var record = await _db.FindAsync(id);
            if (record != null)
            {
                _db.Remove(record);
                return true;
            }
            return false;
        }

        public bool Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entities = await _db.Where(expression).ToListAsync();
            return DeleteRange(entities);
        }

        public bool DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
            return true;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includes = null)
        {
            IQueryable<T> query = _db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includes = null)
        {
            IQueryable<T> query = _db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<bool> InsertAsync(T entity)
        {
            var result = await _db.AddAsync(entity);
            return await Task.FromResult(result.State == EntityState.Added);
        }

        public async Task<bool> InsertRangeAsync(IEnumerable<T> entities)
        {
            //await _db.AddRangeAsync(entities); // this will add child objects
            // https://stackoverflow.com/questions/25441027/how-do-i-stop-entity-framework-from-trying-to-save-insert-child-objects
            // This won't add child objects
            foreach (var entity in entities)
            {
                _db.Attach(entity);
                _context.Entry(entity).State = EntityState.Added;
            }
            return await Task.FromResult(true);
        }

        public bool Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
