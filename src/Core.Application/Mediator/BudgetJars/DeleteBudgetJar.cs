using Core.Application.IRepositories;
using MediatR;


namespace Core.Application.Mediator.BudgetJars
{
    public class DeleteBudgetJarCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public bool IsArchived { get; set; }
        public DeleteBudgetJarCommand(Guid id, bool archive)
        {
            Id = id;
            IsArchived = archive;
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
            var result = false;
            if (request.IsArchived)
            {
                var budgetJar = await repo.GetAsync(x => x.Id == request.Id);
                budgetJar.Archived = true;
                result = repo.Update(budgetJar);
            } else
            {
                result = await repo.DeleteAsync(request.Id);
            }

            if (result)
            {
                await _unitOfWork.SaveAsync();
            }
            return result;
        }
    }
}
