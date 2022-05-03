using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;

namespace Core.Application.Mediator.BudgetJars
{
    public class GetBudgetJarsByUserId : IRequest<List<BudgetJarDto>>
    {
        public Guid UserId { get; set; }
        public GetBudgetJarsByUserId(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetBudgetJarsByUserIdHandler : IRequestHandler<GetBudgetJarsByUserId, List<BudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetBudgetJarsByUserIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BudgetJarDto>> Handle(GetBudgetJarsByUserId request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarRepository;
            var budgetJars = await repo.GetAllAsync(z => z.UserId == request.UserId,
                z => z.OrderBy(o => o.Percentage), new[] { "Icon" });
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
