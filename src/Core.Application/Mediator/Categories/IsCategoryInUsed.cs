using Core.Application.IRepositories;
using MediatR;

namespace Core.Application.Mediator.Categories
{
    public class IsCategoryInUsed : IRequest<bool>
    {
        public Guid Id { get; set; }
        public IsCategoryInUsed(Guid id)
        {
            Id = id;
        }
    }

    public class IsCategoryInUsedHandler : IRequestHandler<IsCategoryInUsed, bool>
    {
        public readonly IUnitOfWork _unitOfWork;

        public IsCategoryInUsedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(IsCategoryInUsed request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ExpenseRepository.ExistsAsync(x => !x.Archived && x.CategoryId == request.Id);
        }
    }
}
