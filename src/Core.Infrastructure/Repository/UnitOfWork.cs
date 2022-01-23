using Core.Domain.Entities;
using Core.Application.IRepositories;
using Core.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Subscription> _SubscriptionRepository;
        private IGenericRepository<Expense> _expenseRepository;
        private IGenericRepository<Category> _categoryRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Subscription> SubscriptionRepository => _SubscriptionRepository ??= new GenericRepository<Subscription>(_context);
        public IGenericRepository<Expense> ExpenseRepository => _expenseRepository ??= new GenericRepository<Expense>(_context);
        public IGenericRepository<Category> CategoryRepository => _categoryRepository ??= new GenericRepository<Category>(_context);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
            // unload / detach all entities
            _context.ChangeTracker.Clear();
        }
    }
}
