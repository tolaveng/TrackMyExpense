using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class ExpenseGroupDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        public bool IsSystem { get; set; }

        public Guid IconId { get; set; }
        public IconDto? Icon { get; set; }

        public static ExpenseGroupDto Clone(ExpenseGroupDto expenseGroupDto)
        {
            return new ExpenseGroupDto() {
                Id = expenseGroupDto.Id,
                UserId = expenseGroupDto.UserId,
                Name = expenseGroupDto.Name,
                IsSystem = expenseGroupDto.IsSystem,
                IconId = expenseGroupDto.IconId,
                Icon = expenseGroupDto.Icon,
            };
        }
    }

    public class ExpenseGroupValidator : BasicValidator<ExpenseGroupDto>
    {
        public ExpenseGroupValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
