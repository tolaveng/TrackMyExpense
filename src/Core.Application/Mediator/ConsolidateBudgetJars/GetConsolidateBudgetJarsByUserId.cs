using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;

namespace Core.Application.Mediator.ConsolidateBudgetJars
{
    public class GetConsolidateBudgetJarsByUserId :IRequest<List<ConsolidateBudgetJarDto>>
    {
        public Guid UserId { get; set; }
        public GetConsolidateBudgetJarsByUserId(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetConsolidateBudgetJarsByUserIdHander : IRequestHandler<GetConsolidateBudgetJarsByUserId, List<ConsolidateBudgetJarDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetConsolidateBudgetJarsByUserIdHander(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ConsolidateBudgetJarDto>> Handle(GetConsolidateBudgetJarsByUserId request, CancellationToken cancellationToken)
        {
            var userBudgetJars = await _unitOfWork.ConsolidateBudgetJarRepository.GetAllAsync(x => x.UserId == request.UserId
                , null, new[] { "Icon" });
            return _mapper.Map<List<ConsolidateBudgetJarDto>>(userBudgetJars);
        }
    }
}
