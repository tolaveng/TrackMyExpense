using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Icons
{
    public class DeleteIconCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteIconCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteIconCommandHandler : IRequestHandler<DeleteIconCommand, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteIconCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteIconCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _unitOfWork.IconRepository.DeleteAsync(request.Id);
            if (deleted)
            {
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
