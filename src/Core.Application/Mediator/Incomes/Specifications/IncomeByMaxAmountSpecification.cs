using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByMaxAmountSpecification : BaseSpecification<Income>
    {
        public IncomeByMaxAmountSpecification(decimal maxAmount) : base()
        {
            FilterExpression = x => x.Amount <= maxAmount;
        }
    }
}
