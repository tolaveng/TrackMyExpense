using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;


namespace Core.Application.Mediator.BudgetJars
{
    public class GetDefaultBudgetJars : IRequest<List<BudgetJarDto>>
    {
    }
    public class GetDefaultBudgetJarsHandler : IRequestHandler<GetDefaultBudgetJars, List<BudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetDefaultBudgetJarsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BudgetJarDto>> Handle(GetDefaultBudgetJars request, CancellationToken cancellationToken)
        {
            var budgetJars = await _unitOfWork.BudgetJarRepository.GetAllAsync(x => !x.Archived && x.IsDefault, null, new[] { "Icon" });
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
