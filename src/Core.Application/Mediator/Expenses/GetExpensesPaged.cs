using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Utils;
using Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Core.Application.Mediator.Expenses
{
    public class GetExpensesPaged : IRequest<PagedResponse<ExpenseDto>>
    {
        public string TimeZoneId { get; set; }
        public Guid UserId { get; set; }
        public Pagination Pagination { get; set; } = new Pagination();

        public GetExpensesPaged(Guid userId, string timeZoneId, Pagination pagination)
        {
            TimeZoneId = timeZoneId;
            UserId = userId;
            Pagination = pagination;
        }
    }

    public class GetExpensesPagedHandler : IRequestHandler<GetExpensesPaged, PagedResponse<ExpenseDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetExpensesPagedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ExpenseDto>> Handle(GetExpensesPaged request, CancellationToken cancellationToken)
        {
            Func<IQueryable<Expense>, IOrderedQueryable<Expense>> orderBy = x => x.OrderByDescending(z => z.PaidDate);
            switch (request.Pagination.SortBy)
            {
                case "PaidDate":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.PaidDate)
                    : orderBy = x => x.OrderByDescending(z => z.PaidDate);
                    break;

                case "BudgetJar":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.BudgetJarId)
                    : orderBy = x => x.OrderByDescending(z => z.BudgetJarId); // TODO: sort by string
                    break;

                case "Amount":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Amount)
                    : orderBy = x => x.OrderByDescending(z => z.Amount);
                    break;
            }

            Expression<Func<Expense, bool>> expression = (z) => z.UserId == request.UserId && !z.Archived;
            var count = await _unitOfWork.ExpenseRepository.CountAsync(expression);
            var data = await _unitOfWork.ExpenseRepository.GetPagedAsync(request.Pagination.Page, request.Pagination.PageSize,
                expression, orderBy, new []{"BudgetJar", "ExpenseGroup" });
            foreach (var item in data)
            {
                item.PaidDate = DateTimeUtil.ToTimeZoneDateTime(item.PaidDate, request.TimeZoneId);
            }
            return new PagedResponse<ExpenseDto>(_mapper.Map<IEnumerable<ExpenseDto>>(data), count);
        }
    }
}
