using FluentValidation;

namespace Core.Application.Models
{
    public class BudgetJarDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;
        public float Percentage { get; set; }
        public Guid IconId { get; set; }

        public Decimal TotalBalance { get; set; }
        public IconDto Icon { get; set; } = new IconDto();
        public bool IsDefault { get; set; }
        public bool Archived { get; set; }

        public static BudgetJarDto Clone(BudgetJarDto budgetJar)
        {
            return new BudgetJarDto()
            {
                Id = budgetJar.Id,
                UserId = budgetJar.UserId,
                Name = budgetJar.Name,
                Percentage = budgetJar.Percentage,
                TotalBalance = budgetJar.TotalBalance,
                IconId = budgetJar.IconId,
                Icon = budgetJar.Icon,
                IsDefault = budgetJar.IsDefault,
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
