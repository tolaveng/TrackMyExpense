using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;

namespace Core.Application.Mediator.BudgetJarTemplates
{
    public class GetBudgetJarTemplatesQuery : IRequest<List<BudgetJarTemplateDto>>
    {
        public bool IsSystem { get; set; }
        public Guid? UserId { get; set; }
        public GetBudgetJarTemplatesQuery(bool isSystem, Guid? userId = null)
        {
            IsSystem = isSystem;
            UserId = userId;
        }
    }
    public class GetBudgetJarTemplatesHandler : IRequestHandler<GetBudgetJarTemplatesQuery, List<BudgetJarTemplateDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetBudgetJarTemplatesHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BudgetJarTemplateDto>> Handle(GetBudgetJarTemplatesQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarTemplateRepository;
            var budgetJars = await repo.GetAllAsync(z => (z.IsSystem || !request.IsSystem) &&
                (!request.UserId.HasValue || z.UserId == request.UserId.Value)
                , null, new [] {"Icon"});
            return _mapper.Map<List<BudgetJarTemplateDto>>(budgetJars);
        }
    }
}
