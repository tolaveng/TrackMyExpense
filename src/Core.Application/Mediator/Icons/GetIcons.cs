using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Providers.IProviders;
using MediatR;
namespace Core.Application.Mediator.Icons
{
    public class GetIconsQuery : IRequest<List<IconDto>>
    {
        public bool IncludeArchived { get; set; }
        public GetIconsQuery(bool includeArchived = false)
        {
            IncludeArchived = includeArchived;
        }
    }
    public class GetIconsHandler : IRequestHandler<GetIconsQuery, List<IconDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetIconsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<IconDto>> Handle(GetIconsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.IconRepository;
            var icons = await repo.GetAllAsync(x => x.Archived || !request.IncludeArchived,
                x => x.OrderBy(o => o.IconType).ThenBy(o => o.Name));
            
            return _mapper.Map<List<IconDto>>(icons);
        }
    }
}
