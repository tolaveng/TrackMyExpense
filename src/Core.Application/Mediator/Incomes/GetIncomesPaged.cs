using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Utils;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Incomes
{
    public class GetIncomesPaged : IRequest<PagedResponse<IncomeDto>>
    {
        public string TimeZoneId { get; set; }
        public Guid UserId { get; set; }
        public Pagination Pagination { get; set; } = new Pagination();

        public GetIncomesPaged(Guid userId, string timeZoneId, Pagination pagination)
        {
            TimeZoneId = timeZoneId;
            UserId = userId;
            Pagination = pagination;
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

            Func<IQueryable<Income>, IOrderedQueryable<Income>> orderBy = x => x.OrderByDescending(z => z.Begin);
            switch (request.Pagination.SortBy)
            {
                case "Begin":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Begin)
                    : orderBy = x => x.OrderByDescending(z => z.Begin);
                    break;

                case "End":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.End)
                    : orderBy = x => x.OrderByDescending(z => z.End);
                    break;

                case "Amount":
                    orderBy = request.Pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Amount)
                    : orderBy = x => x.OrderByDescending(z => z.Amount);
                    break;
            }

            Expression<Func<Income, bool>> expression = (z) => z.UserId == request.UserId && !z.Archived;
            var count = await repo.CountAsync(expression);
            var data = await repo.GetPagedAsync(request.Pagination.Page, request.Pagination.PageSize, expression, orderBy);
            foreach(var item in data)
            {
                item.Begin = DateTimeUtil.ToTimeZoneDateTime(item.Begin, request.TimeZoneId);
                item.End = DateTimeUtil.ToTimeZoneDateTime(item.End, request.TimeZoneId);
            }
            return new PagedResponse<IncomeDto>(_mapper.Map<IEnumerable<IncomeDto>>(data), count);
        }
    }
}
