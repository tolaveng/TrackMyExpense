using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Mediator.BudgetJarTemplates
{
    public class CreateBudgetJarTemplatesFromSystem : IRequest<List<BudgetJarTemplateDto>>
    {
        public Guid UserId { get; set; }
        public CreateBudgetJarTemplatesFromSystem(Guid userId)
        {
            UserId = userId;
        }
    }
    public class CreateBudgetJarTemplatesFromSystemHander : IRequestHandler<CreateBudgetJarTemplatesFromSystem, List<BudgetJarTemplateDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public CreateBudgetJarTemplatesFromSystemHander(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BudgetJarTemplateDto>> Handle(CreateBudgetJarTemplatesFromSystem request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarTemplateRepository;
            var userBudgetJars = await repo.GetAllAsync(x => !x.IsSystem && !x.Archived && x.UserId == request.UserId
                , null, new[] { "Icon" });

            if (!userBudgetJars.Any())
            {
                var systemTemplates = await repo.GetAllAsync(x => x.IsSystem && !x.Archived, null, new[] { "Icon" });
                userBudgetJars = systemTemplates.Select(z => new BudgetJarTemplate()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Name = z.Name,
                    Percentage = z.Percentage,
                    IsSystem = false,
                    IconId = z.IconId,
                    Icon = z.Icon,
                });

                await repo.InsertRangeAsync(userBudgetJars);
                await _unitOfWork.SaveAsync();
            }
            return _mapper.Map<List<BudgetJarTemplateDto>>(userBudgetJars);
        }
    }
}
