using Core.Application.IRepositories;
using MediatR;

namespace Core.Application.Mediator.Categories
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }

        public bool IsArchived { get; set; }

        public DeleteCategoryCommand(Guid id, bool isArchived)
        {
            Id = id;
            IsArchived = isArchived;
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
            var category = await repo.GetAsync(x => x.Id == request.Id);
            if (category == null)
            {
                throw new ArgumentException("Category is not found");
            }

            if (request.IsArchived)
            {
                category.Archived = true;
                repo.Update(category);
            } else
            {
                await repo.DeleteAsync(request.Id);
            }

            try
            {
                await _unitOfWork.SaveAsync();
                return true;

            } catch (Exception)
            {
                // ignored
            }
            
            return false;
        }
    }
}
