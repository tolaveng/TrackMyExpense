using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Subscriptions
{
    public class SaveSubscriptionCommand : IRequest<Guid>
    {
        public SubscriptionDto SubscriptionDto { get; set; }
    }

    public class SaveSubscriptionHandler : IRequestHandler<SaveSubscriptionCommand, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveSubscriptionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork =unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(SaveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = _mapper.Map<Subscription>(request.SubscriptionDto);

            if (subscription.PaidDate.HasValue && subscription.PaidDate.Value.Kind != DateTimeKind.Utc)
            {
                subscription.PaidDate = DateTime.SpecifyKind(subscription.PaidDate.Value, DateTimeKind.Utc);
            }
            if (subscription.ValidFrom.HasValue && subscription.ValidFrom.Value.Kind != DateTimeKind.Utc)
            {
                subscription.ValidFrom = DateTime.SpecifyKind(subscription.ValidFrom.Value, DateTimeKind.Utc);
            }
            if (subscription.ValidTo.HasValue && subscription.ValidTo.Value.Kind != DateTimeKind.Utc)
            {
                subscription.ValidTo = DateTime.SpecifyKind(subscription.ValidTo.Value, DateTimeKind.Utc);
            }

            if (subscription.Id == Guid.Empty)
            {
                subscription.Id = Guid.NewGuid();
                await _unitOfWork.SubscriptionRepository.InsertAsync(subscription);
            } else
            {
                _unitOfWork.SubscriptionRepository.Update(subscription);
            }
            
            await _unitOfWork.SaveAsync();

            return subscription.Id;
        }
    }
}
