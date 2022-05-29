using Core.Domain.Entities;
using Core.Application.IRepositories;
using Core.Infrastructure.Database;
using System;
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
        private IGenericRepository<IncomeBudgetJar> _incomeBudgetJarRepository;
        private IGenericRepository<Subscription> _subscriptionRepository;
        private IGenericRepository<Income> _incomeRepository;
        private IGenericRepository<Expense> _expenseRepository;
        private IGenericRepository<Attachment> _attachmentRepository;
        private IGenericRepository<ExpenseGroup> _expenseGroupRepository;
        private IGenericRepository<Icon> _iconRepository;
        private IGenericRepository<Currency> _currencyRepository;

        public UnitOfWork(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }

        public IGenericRepository<SysAttribute> SysAttributeRepository => _sysAttributeRepository ??= new GenericRepository<SysAttribute>(_context);
        public IGenericRepository<PageHtml> PageHtmlRepository => _pageHtmlRepository ??= new GenericRepository<PageHtml>(_context);
        public IGenericRepository<BudgetJar> BudgetJarRepository => _budgetJarRepository ??= new GenericRepository<BudgetJar>(_context);
        public IGenericRepository<IncomeBudgetJar> IncomeBudgetJarRepository => _incomeBudgetJarRepository ??= new GenericRepository<IncomeBudgetJar>(_context);
        public IGenericRepository<Subscription> SubscriptionRepository => _subscriptionRepository ??= new GenericRepository<Subscription>(_context);
        public IGenericRepository<Income> IncomeRepository => _incomeRepository ??= new GenericRepository<Income>(_context);
        public IGenericRepository<Expense> ExpenseRepository => _expenseRepository ??= new GenericRepository<Expense>(_context);
        public IGenericRepository<Attachment> AttachmentRepository => _attachmentRepository ??= new GenericRepository<Attachment>(_context);
        public IGenericRepository<ExpenseGroup> ExpenseGroupRepository => _expenseGroupRepository ??= new GenericRepository<ExpenseGroup>(_context);
        public IGenericRepository<Icon> IconRepository => _iconRepository ??= new GenericRepository<Icon>(_context);

        public IGenericRepository<Currency> CurrencyRepository => _currencyRepository ??= new GenericRepository<Currency>(_context);

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
