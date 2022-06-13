using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<SysAttribute> SysAttributeRepository { get; }
        IGenericRepository<PageHtml> PageHtmlRepository { get; }
        IGenericRepository<Subscription> SubscriptionRepository { get; }
        IGenericRepository<BudgetJar> BudgetJarRepository { get; }
        IGenericRepository<IncomeBudgetJar> IncomeBudgetJarRepository { get; }
        IGenericRepository<Income> IncomeRepository { get; }
        IGenericRepository<Expense> ExpenseRepository { get; }
        IGenericRepository<Attachment> AttachmentRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Icon> IconRepository { get; }
        IGenericRepository<Currency> CurrencyRepository { get; }
        Task SaveAsync();
    }
}
