using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Subscriptions
{
    public class GetSubscription : IRequest<SubscriptionDto>
    {
        public Guid SubscriptionId { get; set; }
        public GetSubscription(Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
    }

    public class GetSubscriptionHandler : IRequestHandler<GetSubscription, SubscriptionDto>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetSubscriptionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> Handle(GetSubscription request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.SubscriptionRepository.GetAsync(z => z.Id == request.SubscriptionId);
            if (subscription == null) return null;
            return _mapper.Map<SubscriptionDto>(subscription);
        }
    }

}
