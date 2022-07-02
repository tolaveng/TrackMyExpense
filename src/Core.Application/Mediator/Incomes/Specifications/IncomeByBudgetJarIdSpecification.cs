using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByBudgetJarIdSpecification : BaseSpecification<Income>
    {
        public IncomeByBudgetJarIdSpecification(Guid budgetJarId)
            : base()
        {
            FilterExpression = x => x.IncomeBudgetJars.Any(y => y.BudgetJarId == budgetJarId);
        }
    }
}
