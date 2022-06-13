using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Categories
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.CategoryRepository;
            var deleted = await repo.DeleteAsync(request.Id);
            if (deleted)
            {
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
