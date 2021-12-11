using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        //IGenericRepository<Expense> ExpenseRepository { get; set; }
        Task SaveAsync();
    }
}
