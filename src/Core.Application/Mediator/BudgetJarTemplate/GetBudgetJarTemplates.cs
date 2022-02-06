using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.BudgetJars
{
    public class GetBudgetJarTemplatesQuery : IRequest<List<BudgetJarTemplateDto>>
    {
        public bool IsSystem { get; set; }
        public GetBudgetJarTemplatesQuery(bool isSystem)
        {
            IsSystem = isSystem;
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
            var budgetJars = await repo.GetAllAsync(z => z.IsSystem || !request.IsSystem, null, new [] {"Icon"});
            return _mapper.Map<List<BudgetJarTemplateDto>>(budgetJars);
        }
    }
}
