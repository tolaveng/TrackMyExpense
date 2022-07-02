using Core.Application.Specifications.Base;
using Core.Domain.Entities;

namespace Core.Application.Mediator.Incomes.Specifications
{
    public class IncomeByDescriptionSpecification : BaseSpecification<Income>
    {
        public IncomeByDescriptionSpecification(string description)
        {
            FilterExpression = x => x.Note.ToLower().Contains(description.ToLower());
        }
    }
}
