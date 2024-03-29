﻿using AutoMapper;
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

        public ExpenseFilter? Filter { get; set; }

        public GetExpensesPaged(Guid userId, string timeZoneId, Pagination pagination, ExpenseFilter? filter = null)
        {
            TimeZoneId = timeZoneId;
            UserId = userId;
            Pagination = pagination;
            Filter = filter;
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
                    ? orderBy = x => x.OrderBy(z => z.BudgetJar.Name)
                    : orderBy = x => x.OrderByDescending(z => z.BudgetJar.Name);
                    break;

                case "Amount":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Amount)
                    : orderBy = x => x.OrderByDescending(z => z.Amount);
                    break;

                case "Category":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Category.Name)
                    : orderBy = x => x.OrderByDescending(z => z.Category.Name);
                    break;
            }

            Expression<Func<Expense, bool>> expression = x => x.UserId == request.UserId && !x.Archived;

            if (request.Filter != null)
            {
                var properties = typeof(ExpenseFilter).GetProperties();
                foreach (var prop in properties)
                {
                    if (prop == null || prop == default) continue;
                    var value = prop.GetValue(request.Filter, null);
                    if (value == null) continue;

                    var predicate = GetPredicate(prop.Name, value);
                    if (predicate != null)
                    {   
                        expression = LinqUtil.AndAlso(expression, predicate);
                    }
                }
            }

            var count = await _unitOfWork.ExpenseRepository.CountAsync(expression);
            var data = await _unitOfWork.ExpenseRepository.GetPagedAsync(request.Pagination.Page, request.Pagination.PageSize,
                expression, orderBy, new []{"BudgetJar", "Category" });
            // resolve date
            foreach (var item in data)
            {
                item.PaidDate = DateTimeUtil.ToTimeZoneDateTime(item.PaidDate, request.TimeZoneId);
            }
            return new PagedResponse<ExpenseDto>(_mapper.Map<IEnumerable<ExpenseDto>>(data), count);
        }

        private Expression<Func<Expense, bool>>? GetPredicate(string Name, object Value)
        {
            switch (Name)
            {
                case "FromDate":
                    if (Value is DateTime fromDate)
                    {
                        return x =>
                            x.PaidDate >= fromDate.StartOfDayUtc();
                    }
                    break;

                case "ToDate":
                    if (Value is DateTime toDate)
                    {
                        return x =>
                            x.PaidDate <= toDate.EndOfDayUtc();
                    }
                    break;

                case "BudgetJarId":
                    if (Value is Guid budgetJarId && budgetJarId != Guid.Empty)
                    {
                        return x => x.BudgetJarId == budgetJarId;
                    }
                    break;

                case "CategoryId":
                    if (Value is Guid categoryId && categoryId != Guid.Empty) {
                        return x => x.CategoryId == categoryId;
                    }
                    break;

                case "Description":
                    if (Value is string description && !string.IsNullOrWhiteSpace(description))
                    {
                        return x => x.Description.ToLower().Contains(description.ToLower());
                    }
                    break;

                case "MinAmount":
                    if (Value is decimal minAmount && minAmount > -1)
                    {
                        return x => x.Amount >= minAmount;
                    }
                    break;

                case "MaxAmount":
                    if (Value is decimal maxAmount && maxAmount > 0)
                    {
                        return x => x.Amount <= maxAmount;
                    }
                    break;

                case "IsTaxable":
                    if (Value is bool isTaxable && isTaxable)
                    {
                        return x => x.IsTaxable;
                    }
                    break;
            }
            return null;
        }
    }
}
