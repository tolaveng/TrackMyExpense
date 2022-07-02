using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByFromDateSpecification : BaseSpecification<Income>
    {
        public IncomeByFromDateSpecification(DateTime fromDate)
            : base()
        {
            FilterExpression = x => x.FromDate >= fromDate;
        }
    }
}
