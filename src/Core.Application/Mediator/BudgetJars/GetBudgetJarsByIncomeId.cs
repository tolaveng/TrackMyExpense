using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;


namespace Core.Application.Mediator.BudgetJars
{
    public class GetBudgetJarsByIncomeId : IRequest<List<BudgetJarDto>>
    {
        public Guid IncomeId { get; set; }
        public GetBudgetJarsByIncomeId(Guid incomeId)
        {
            IncomeId = incomeId;
        }
    }

    public class GetBudgetJarsByIncomeIdHandler : IRequestHandler<GetBudgetJarsByIncomeId, List<BudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetBudgetJarsByIncomeIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BudgetJarDto>> Handle(GetBudgetJarsByIncomeId request, CancellationToken cancellationToken)
        {
            var budgetJars = await _unitOfWork.BudgetJarRepository.GetAllAsync(z => z.IncomeId == request.IncomeId,
                z => z.OrderBy(o => o.Percentage), new[] { "Icon" });
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
