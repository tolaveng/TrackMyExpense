using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Core.Application.Mediator.IncomeBudgetJars
{
    public class GetCalculatedBudgetJarByDate : IRequest<List<BudgetJarDto>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        
        public GetCalculatedBudgetJarByDate(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }

    public class GetCalculatedBudgetJarByDateHandler : IRequestHandler<GetCalculatedBudgetJarByDate, List<BudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetCalculatedBudgetJarByDateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<BudgetJarDto>> Handle(GetCalculatedBudgetJarByDate request, CancellationToken cancellationToken)
        {   
            // Get expenses
            Expression<Func<Expense, bool>> filterExpense = expense => !expense.Archived &&
                expense.PaidDate >= request.FromDate && expense.PaidDate <= request.ToDate;

            var expenses = (await _unitOfWork.ExpenseRepository.GetAllAsync(filterExpense, null, null))
                .GroupBy(x => x.BudgetJarId, x => x.Amount)
                .ToDictionary(x => x.Key, x => x.Sum());

            // Sum all income between the date range
            Expression<Func<IncomeBudgetJar, bool>> filterIncomeJar = x => !x.Income.Archived &&
                x.Income.FromDate >= request.FromDate && x.Income.ToDate <= request.ToDate;

            var budgetJars = (await _unitOfWork.IncomeBudgetJarRepository.GetAllAsync(filterIncomeJar, null, new[] { "BudgetJar" }))
                .GroupBy(x => x.BudgetJarId)
                .Select(x => new BudgetJarDto()
                {
                    Id = x.Key,
                    Name = x.First().BudgetJar.Name,
                    TotalBalance = x.Sum(y => y.Amount) - (expenses.TryGetValue(x.Key, out var expenseAmount) ? expenseAmount : 0)
                }).ToList();

            return budgetJars;
        }
    }
}
