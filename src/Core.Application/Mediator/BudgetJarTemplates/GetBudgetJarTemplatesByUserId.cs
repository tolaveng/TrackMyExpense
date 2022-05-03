using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;

namespace Core.Application.Mediator.BudgetJarTemplates
{
    public class GetBudgetJarTemplatesByUserId : IRequest<List<BudgetJarTemplateDto>>
    {
        public Guid UserId { get; set; }
        public GetBudgetJarTemplatesByUserId(Guid userId)
        {
            UserId = userId;
        }
    }
    public class GetBudgetJarTemplatesByUserIdHander : IRequestHandler<GetBudgetJarTemplatesByUserId, List<BudgetJarTemplateDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetBudgetJarTemplatesByUserIdHander(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BudgetJarTemplateDto>> Handle(GetBudgetJarTemplatesByUserId request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarTemplateRepository;
            var userBudgetJars = await repo.GetAllAsync(x => !x.IsSystem && !x.Archived && x.UserId == request.UserId
                , null, new[] { "Icon" });
            return _mapper.Map<List<BudgetJarTemplateDto>>(userBudgetJars);
        }
    }
}
