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
            // replace by income jar
            foreach(var jar in budgetJars)
            {
                var incomeJar = incomeJars.Single(x => x.BudgetJarId == jar.Id);
                jar.TotalBalance = incomeJar.Amount;
                jar.Percentage = incomeJar.Percentage;
            }
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
