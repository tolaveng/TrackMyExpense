using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class BudgetJarTemplateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public int Percentage { get; set; }
        public bool IsSystem { get; set; }
        public Guid IconId { get; set; }
        public IconDto Icon { get; set; } = new IconDto();

        public static BudgetJarTemplateDto Clone(BudgetJarTemplateDto budgetJar)
        {
            return new BudgetJarTemplateDto()
            {
                Id = budgetJar.Id,
                UserId = budgetJar.UserId,
                Name = budgetJar.Name,
                Percentage = budgetJar.Percentage,
                IsSystem = budgetJar.IsSystem,
                IconId = budgetJar.IconId,
                Icon = budgetJar.Icon,
            };
        }
    }

    public class BudgetJarTemplateValidator : BasicValidator<BudgetJarTemplateDto>
    {
        public BudgetJarTemplateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Percentage).NotEmpty();
        }
    }
}
