using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByToDateSpecification : BaseSpecification<Income>
    {
        public IncomeByToDateSpecification(DateTime toDate)
            : base()
        {
            FilterExpression = x => x.ToDate <= toDate;
        }
    }
}
