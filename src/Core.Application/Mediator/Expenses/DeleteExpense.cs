using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Expenses
{
    public class DeleteExpense : IRequest<bool>
    {
        public Guid ExpenseId { get; set; }
        public DeleteExpense(Guid expenseId)
        {
            ExpenseId = expenseId;
        }
    }

    public class DeleteExpenseHandler : IRequestHandler<DeleteExpense, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteExpenseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteExpense request, CancellationToken cancellationToken)
        {
            var expense = await _unitOfWork.ExpenseRepository.GetAsync(x => x.Id == request.ExpenseId);
            if (expense == null) return false;
            expense.Archived = true;

            // update balance
            var budgetJar = await _unitOfWork.BudgetJarRepository.GetAsync(x => x.Id == expense.BudgetJarId);
            budgetJar.TotalBalance = budgetJar.TotalBalance + expense.Amount;
            _unitOfWork.BudgetJarRepository.Update(budgetJar);
            
            try
            {
                _unitOfWork.ExpenseRepository.Update(expense);
                await _unitOfWork.SaveAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
