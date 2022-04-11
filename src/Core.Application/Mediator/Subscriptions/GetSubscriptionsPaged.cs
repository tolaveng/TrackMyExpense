using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Subscriptions
{
    public class GetSubscriptionsPaged : IRequest<PagedResponse<SubscriptionDto>>
    {
        public Guid UserId { get; set; }
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class GetPagedSubscriptionsHandler : IRequestHandler<GetSubscriptionsPaged, PagedResponse<SubscriptionDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetPagedSubscriptionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<SubscriptionDto>> Handle(GetSubscriptionsPaged request, CancellationToken cancellationToken)
        {
            var subRepository = _unitOfWork.SubscriptionRepository;
            var pagination = request.Pagination;
            
            Func<IQueryable<Subscription>, IOrderedQueryable<Subscription>> orderBy = x => x.OrderByDescending(z => z.Id);
            switch (pagination.SortBy)
            {
                case "SubscriptionType":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.SubscriptionType)
                    : orderBy = x => x.OrderByDescending(z => z.SubscriptionType);
                    break;

                case "PaidDate":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.PaidDate)
                    : orderBy = x => x.OrderByDescending(z => z.PaidDate);
                    break;

                case "Begin":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.Begin)
                    : orderBy = x => x.OrderByDescending(z => z.Begin);
                    break;

                case "End":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.End)
                    : orderBy = x => x.OrderByDescending(z => z.End);
                    break;

                case "IsCanceled":
                    orderBy = pagination.SortDirection == Pagination.Ascending
                    ? orderBy = x => x.OrderBy(z => z.IsCanceled)
                    : orderBy = x => x.OrderByDescending(z => z.IsCanceled);
                    break;
            }

            Expression<Func<Subscription, bool>> expression = (z) => z.UserId == request.UserId;

            var count = await subRepository.CountAsync(expression);
            var data = await subRepository.GetPagedAsync(pagination.Page, pagination.PageSize, expression, orderBy);
            return new PagedResponse<SubscriptionDto>(_mapper.Map<IEnumerable<SubscriptionDto>>(data), count);
        }
    }

}
