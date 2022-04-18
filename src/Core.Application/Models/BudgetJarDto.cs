using FluentValidation;

namespace Core.Application.Models
{
    public class BudgetJarDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? IncomeId { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Percentage { get; set; }
        public Decimal Amount { get; set; }
        public Guid IconId { get; set; }
        public IconDto Icon { get; set; } = new IconDto();

        public static BudgetJarDto Clone(BudgetJarDto budgetJar)
        {
            return new BudgetJarDto()
            {
                Id = budgetJar.Id,
                UserId = budgetJar.UserId,
                IncomeId = budgetJar.IncomeId,
                Name = budgetJar.Name,
                Percentage = budgetJar.Percentage,
                Amount = budgetJar.Amount,
                IconId = budgetJar.IconId,
                Icon = budgetJar.Icon,
            };
        }

        public static BudgetJarDto FromTemplate(BudgetJarTemplateDto template)
        {
            return new BudgetJarDto()
            {
                Id = Guid.NewGuid(),
                UserId = template.UserId,
                Name = template.Name,
                Percentage = template.Percentage,
                IconId = template.IconId,
                Icon = template.Icon,
            };
        }
    }

    public class BudgetJarValidator : BasicValidator<BudgetJarDto>
    {
        public BudgetJarValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Percentage).NotEmpty();
        }
    }
}
