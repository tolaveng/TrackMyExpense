using Core.Domain.Entities;
using Core.Application.IRepositories;
using Core.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<SysAttribute> _sysAttributeRepository;
        private IGenericRepository<PageHtml> _pageHtmlRepository;
        private IGenericRepository<BudgetJar> _budgetJarRepository;
        private IGenericRepository<Subscription> _subscriptionRepository;
        private IGenericRepository<Expense> _expenseRepository;
        private IGenericRepository<Category> _categoryRepository;

        public UnitOfWork(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IGenericRepository<SysAttribute> SysAttributeRepository => _sysAttributeRepository ??= new GenericRepository<SysAttribute>(_context);
        public IGenericRepository<PageHtml> PageHtmlRepository => _pageHtmlRepository ??= new GenericRepository<PageHtml>(_context);
        public IGenericRepository<BudgetJar> BudgetJarRepository => _budgetJarRepository ??= new GenericRepository<BudgetJar>(_context);
        public IGenericRepository<Subscription> SubscriptionRepository => _subscriptionRepository ??= new GenericRepository<Subscription>(_context);
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
