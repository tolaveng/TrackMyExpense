using Core.Application.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Categories
{
    public class DeleteExpenseGroupCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public DeleteExpenseGroupCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteExpenseGroupHandler : IRequestHandler<DeleteExpenseGroupCommand, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteExpenseGroupHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteExpenseGroupCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.ExpenseGroupRepository;
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
