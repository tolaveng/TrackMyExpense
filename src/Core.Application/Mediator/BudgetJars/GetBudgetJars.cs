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
    public class GetBudgetJarsQuery : IRequest<List<BudgetJarDto>>
    {
        public bool IncludeArchived { get; set; }
        public GetBudgetJarsQuery(bool includeArchived = false)
        {
            IncludeArchived = includeArchived;
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
            var budgetJars = await repo.GetAllAsync(z => z.Archived || !request.IncludeArchived);
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
