using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Mediator.Incomes.Specifications;
using Core.Application.Models;
using Core.Application.Specifications.Base;
using Core.Application.Utils;
using Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;


namespace Core.Application.Mediator.Incomes
{
    public class GetIncomesPaged : IRequest<PagedResponse<IncomeDto>>
    {
        public string TimeZoneId { get; set; }
        public Guid UserId { get; set; }
        public Pagination Pagination { get; set; } = new Pagination();

        public IncomeFilter? Filter { get; set; }

        public GetIncomesPaged(Guid userId, string timeZoneId, Pagination pagination, IncomeFilter? filter = null)
        {
            TimeZoneId = timeZoneId;
            UserId = userId;
            Pagination = pagination;
            Filter = filter;
        }
    }

    public class GetIncomesPagedHandler : IRequestHandler<GetIncomesPaged, PagedResponse<IncomeDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetIncomesPagedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IncomeDto>> Handle(GetIncomesPaged request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.IncomeRepository;

            Func<IQueryable<Income>, IOrderedQueryable<Income>> orderBy = x => x.OrderByDescending(z => z.FromDate);
            switch (request.Pagination.SortBy)
            {
                case "IncomePeriod":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.FromDate)
                    : orderBy = x => x.OrderByDescending(z => z.FromDate);
                    break;

                case "Amount":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Amount)
                    : orderBy = x => x.OrderByDescending(z => z.Amount);
                    break;
            }

            var userIdSpecification = new IncomeByUserIdSpecification(request.UserId);
            var filterSpecification = new BaseSpecification<Income>(userIdSpecification.FilterExpression);

            if (request.Filter != null)
            {
                var filter = request.Filter;
                if (!string.IsNullOrWhiteSpace(filter.Note))
                {
                    filterSpecification = filterSpecification.And(new IncomeByDescriptionSpecification(filter.Note));
                }

                if (filter.MinAmount.HasValue && filter.MinAmount.Value > -1)
                {
                    filterSpecification = filterSpecification.And(new IncomeByMinAmountSpecification(filter.MinAmount.Value));
                }

                if (filter.MaxAmount.HasValue && filter.MaxAmount.Value > 0)
                {
                    filterSpecification = filterSpecification.And(new IncomeByMaxAmountSpecification(filter.MaxAmount.Value));
                }

                if (filter.BudgetJarId != Guid.Empty)
                {
                    filterSpecification = filterSpecification.And(new IncomeByBudgetJarIdSpecification(filter.BudgetJarId));
                }

                if (filter.FromDate.HasValue)
                {
                    filterSpecification = filterSpecification.And(new IncomeByFromDateSpecification(filter.FromDate.Value));
                }

                if (filter.ToDate.HasValue)
                {
                    filterSpecification = filterSpecification.And(new IncomeByToDateSpecification(filter.ToDate.Value));
                }

            }
            Expression<Func<Income, bool>> expression = filterSpecification.FilterExpression;

            var count = await repo.CountAsync(expression);
            var data = await repo.GetPagedAsync(request.Pagination.Page, request.Pagination.PageSize, expression, orderBy, new[] { "IncomeBudgetJars" });
            foreach(var item in data)
            {
                item.FromDate = DateTimeUtil.ToTimeZoneDateTime(item.FromDate, request.TimeZoneId);
                item.ToDate = DateTimeUtil.ToTimeZoneDateTime(item.ToDate, request.TimeZoneId);
            }
            return new PagedResponse<IncomeDto>(_mapper.Map<IEnumerable<IncomeDto>>(data), count);
        }
    }
}
