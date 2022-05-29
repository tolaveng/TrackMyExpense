using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Providers.IProviders;
using MediatR;
using System;

namespace Core.Application.Mediator.Icons
{
    public class GetIconsByIds : IRequest<List<IconDto>>
    {
        public GetIconsByIds(List<Guid> iconIds)
        {
            IconIds = iconIds;
        }

        public List<Guid> IconIds { get; }
    }

    public class GetIconsByIdsHandler : IRequestHandler<GetIconsByIds, List<IconDto>>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetIconsByIdsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<IconDto>> Handle(GetIconsByIds request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.IconRepository;
            var icons = await repo.GetAllAsync(x => request.IconIds.Contains(x.Id));

            return _mapper.Map<List<IconDto>>(icons);
        }
    }
}
