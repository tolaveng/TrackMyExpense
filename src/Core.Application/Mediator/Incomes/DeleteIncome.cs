using Core.Application.IRepositories;
using MediatR;

namespace Core.Application.Mediator.Incomes
{
    public class DeleteIncome : IRequest<bool>
    {
        public Guid IncomeId { get; set; }
        public DeleteIncome(Guid incomeId)
        {
            IncomeId = incomeId;
        }
    }

    public class DeleteIncomeHandler : IRequestHandler<DeleteIncome, bool>
    {
        public IUnitOfWork _unitOfWork;
        public DeleteIncomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteIncome request, CancellationToken cancellationToken)
        {
            var income = await _unitOfWork.IncomeRepository.GetAsync(x => x.Id == request.IncomeId);
            if (income == null) return false;

            income.Archived = true;
            try
            {
                _unitOfWork.IncomeRepository.Update(income);
                await _unitOfWork.SaveAsync();
                return true;

            } catch (Exception)
            {
                return false;
            }
        }
    }
}
