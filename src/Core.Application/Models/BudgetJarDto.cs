﻿using FluentValidation;

namespace Core.Application.Models
{
    public class BudgetJarDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? IncomeId { get; set; }

        public string Name { get; set; }
        public int Percentage { get; set; }
        public Decimal Amount { get; set; }
        public Guid IconId { get; set; }
        public IconDto Icon { get; set; } = new IconDto();

        public static BudgetJarDto Copy(BudgetJarDto budgetJar)
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
