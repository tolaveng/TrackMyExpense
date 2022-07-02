using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByUserIdSpecification : BaseSpecification<Income>
    {
        public IncomeByUserIdSpecification(Guid userId) : base()
        {
            FilterExpression = x => x.UserId == userId && !x.Archived;
        }
    }
}
