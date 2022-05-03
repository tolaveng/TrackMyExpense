using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;

namespace Core.Application.Mediator.BudgetJars
{
    public class GetBudgetJarsQuery : IRequest<List<BudgetJarDto>>
    {
        public GetBudgetJarsQuery()
        {
        }
    }
    public class GetBudgetJarsHandler : IRequestHandler<GetBudgetJarsQuery, List<BudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetBudgetJarsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BudgetJarDto>> Handle(GetBudgetJarsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarRepository;
            var budgetJars = await repo.GetAllAsync();
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
