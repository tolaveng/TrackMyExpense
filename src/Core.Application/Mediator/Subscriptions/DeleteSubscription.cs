using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Subscriptions
{
    public class DeleteSubscriptionCommand : IRequest<bool>
    {
        public Guid SubscriptionId { get; }
        public DeleteSubscriptionCommand(Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
    }

    public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscriptionCommand, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteSubscriptionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subRespository = _unitOfWork.SubscriptionRepository;
            var deleted = await subRespository.DeleteAsync(request.SubscriptionId);
            if (deleted)
            {
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
