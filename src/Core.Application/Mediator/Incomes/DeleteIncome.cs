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

            var incomeBudgetJars = await _unitOfWork.IncomeBudgetJarRepository.GetAllAsync(x => x.IncomeId == request.IncomeId);
            var budgetJarIds = incomeBudgetJars.Select(x => x.BudgetJarId).ToArray();
            var budgetJars = await _unitOfWork.BudgetJarRepository.GetAllAsync(x => budgetJarIds.Contains(x.Id));
            foreach(var incomeBudgetJar in incomeBudgetJars)
            {
                var budgetJar = budgetJars.SingleOrDefault(x => x.Id == incomeBudgetJar.BudgetJarId);
                if (budgetJar == null) continue;
                budgetJar.TotalBalance = budgetJar.TotalBalance - incomeBudgetJar.Amount;
                _unitOfWork.BudgetJarRepository.Update(budgetJar);
            }
            
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
