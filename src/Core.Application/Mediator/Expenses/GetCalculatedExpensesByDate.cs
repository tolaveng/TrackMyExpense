using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Core.Application.Mediator.Expenses
{
    public class GetCalculatedExpensesByDate : IRequest<List<ExpenseDto>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public GetCalculatedExpensesByDate(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        public class GetCalculatedExpensesByDateHandler : IRequestHandler<GetCalculatedExpensesByDate, List<ExpenseDto>>
        {
            private readonly IMapper _mapper;
            public readonly IUnitOfWork _unitOfWork;

            public GetCalculatedExpensesByDateHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<ExpenseDto>> Handle(GetCalculatedExpensesByDate request, CancellationToken cancellationToken)
            {
                Expression<Func<Expense, bool>> filterExpense = expense => !expense.Archived &&
                    expense.PaidDate >= request.FromDate && expense.PaidDate <= request.ToDate;

                var expenses = (await _unitOfWork.ExpenseRepository.GetAllAsync(filterExpense, null, null))
                    .GroupBy(x => x.PaidDate)
                    .Select(x => new ExpenseDto()
                    {
                        PaidDate = x.Key,
                        Amount = x.Sum(y => y.Amount),
                    })
                    .ToList();
                return expenses;
            }
        }
    }
}
