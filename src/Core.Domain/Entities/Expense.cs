using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Expense : AuditableEntity<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public DateTime PaidDate { get; set; }
        public string Ref { get; set; }
        public string Payee { get; set; }
        public bool IsTaxable { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public Guid BudgetJarId { get; set; }
        public virtual BudgetJar BudgetJar { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public int? RecurrentExpenseId { get; set; }
    }
}
