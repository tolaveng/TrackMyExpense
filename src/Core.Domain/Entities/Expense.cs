using Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Expense : AuditableEntity<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaidDate { get; set; }
        public string Ref { get; set; }
        public string Payee { get; set; }
        public bool IsTaxable { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public Guid BudgetJarId { get; set; }
        public virtual BudgetJar BudgetJar { get; set; }
        public Guid ExpenseGroupId { get; set; }
        public virtual ExpenseGroup ExpenseGroup { get; set; }

        public int? RecurrentExpenseId { get; set; }
    }
}
