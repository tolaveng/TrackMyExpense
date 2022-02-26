using AutoMapper;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Icons
{
    public class SaveIconCommand : IRequest<Guid>
    {
        public IconDto IconDto { get; set; }
        public bool IsNew { get; set; }
        public SaveIconCommand(IconDto iconDto, bool isNew)
        {
            IconDto = iconDto;
            IsNew = isNew;
        }
    }

    public class SaveIconHandler : IRequestHandler<SaveIconCommand, Guid>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveIconHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SaveIconCommand request, CancellationToken cancellationToken)
        {
            var icon = _mapper.Map<Icon>(request.IconDto);
            if (icon.Id == Guid.Empty || icon.Id == null) throw new ArgumentException("Id cannot be empty");
            
            if (request.IsNew)
            {
                await _unitOfWork.IconRepository.InsertAsync(icon);
            }
            else
            {
                _unitOfWork.IconRepository.Update(icon);
            }

            await _unitOfWork.SaveAsync();

            return icon.Id;
        }
    }
}
