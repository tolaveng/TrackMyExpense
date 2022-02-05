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
        public bool IsSystem { get; set; }
        public GetBudgetJarsQuery(bool isSystem)
        {
            IsSystem = isSystem;
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
            var budgetJars = await repo.GetAllAsync(z => z.IsSystem || !request.IsSystem);
            return _mapper.Map<List<BudgetJarDto>>(budgetJars);
        }
    }
}
