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
        IGenericRepository<Expense> ExpenseRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        Task SaveAsync();
    }
}
