using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Categories
{
    public class GetExpenseGroupsQuery : IRequest<List<ExpenseGroupDto>>
    {
        public bool IsSystem { get; set; }
        public Guid? UserId { get; set; }
        public GetExpenseGroupsQuery(bool isSystem, Guid? userId = null)
        {
            IsSystem = isSystem;
            UserId = userId;
        }
    }
    public class GetExpenseGroupsHandler : IRequestHandler<GetExpenseGroupsQuery, List<ExpenseGroupDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetExpenseGroupsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ExpenseGroupDto>> Handle(GetExpenseGroupsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.ExpenseGroupRepository;
            var expenseGroups = await repo.GetAllAsync(x => (x.IsSystem || !request.IsSystem) &&
            (!request.UserId.HasValue || x.UserId == request.UserId),
                x => x.OrderBy(o => o.Name), new [] {"Icon"});
            return _mapper.Map<List<ExpenseGroupDto>>(expenseGroups);
        }
    }
}
