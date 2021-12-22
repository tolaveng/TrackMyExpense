using Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Expense : AuditableEntity
    {
        public long ExpenseId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public DateTime PayDate { get; set; }
        public string Ref { get; set; }
        public string Payee { get; set; }
        public bool IsTaxable { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string[] Images { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
