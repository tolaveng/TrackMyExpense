using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Icons
{
    public class GetIconRequest : IRequest<IconDto>
    {
        public Guid Id { get; set; }
        public GetIconRequest(Guid id)
        {
            Id = id;
        }
    }

    public class GetIconRequestHandler : IRequestHandler<GetIconRequest, IconDto?>
    {
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public GetIconRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IconDto?> Handle(GetIconRequest request, CancellationToken cancellationToken)
        {
            var icon = await _unitOfWork.IconRepository.GetAsync(z => z.Id == request.Id);
            if (icon == null) return null;
            return _mapper.Map<IconDto>(icon);
        }
    }
}
