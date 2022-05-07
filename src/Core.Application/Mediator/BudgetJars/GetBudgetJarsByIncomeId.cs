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
            var incomeJars = await _unitOfWork.IncomeBudgetJarRepository.GetAllAsync(x => x.IncomeId == request.IncomeId);
            var jarIds = incomeJars.Select(x => x.BudgetJarId).ToList();
            var budgetJars = await _unitOfWork.BudgetJarRepository.GetAllAsync(x => jarIds.Contains(x.Id),
                x => x.OrderByDescending(o => o.Percentage), new[] {"Icon"});
            foreach(var jar in budgetJars)
            {
                jar.TotalBalance = 0;
                jar.Percentage = incomeJars.Single(x => x.BudgetJarId == jar.Id).Percentage;
            }
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
