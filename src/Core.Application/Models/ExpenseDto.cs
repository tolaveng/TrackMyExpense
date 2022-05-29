using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class ExpenseDto : Auditable<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = String.Empty;
        public DateTime PaidDate { get; set; }
        public bool IsTaxable { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
        public Guid BudgetJarId { get; set; }
        public BudgetJarDto BudgetJar { get; set; } = new BudgetJarDto();
        public Guid ExpenseGroupId { get; set; }
        public ExpenseGroupDto ExpenseGroup { get; set; } = new ExpenseGroupDto();

        public int? RecurrentExpenseId { get; set; }
    }
}
