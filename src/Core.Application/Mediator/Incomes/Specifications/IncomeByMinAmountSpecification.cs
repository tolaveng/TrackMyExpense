using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByMinAmountSpecification : BaseSpecification<Income>
    {
        public IncomeByMinAmountSpecification(decimal minAmount) : base()
        {
            FilterExpression = x => x.Amount >= minAmount;
        }
    }
}
