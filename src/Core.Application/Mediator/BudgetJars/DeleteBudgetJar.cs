using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.BudgetJars
{
    public class DeleteBudgetJarCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public DeleteBudgetJarCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteBudgetJarHandler : IRequestHandler<DeleteBudgetJarCommand, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteBudgetJarHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteBudgetJarCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.BudgetJarRepository;
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
