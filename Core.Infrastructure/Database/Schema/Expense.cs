using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Schema
{
    public class Expense : IArchivable
    {
        [Key]
        public long ExpenseId { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public DateTime PayDate { get; set; }
        public string Ref { get; set; }
        public string Payee { get; set; }
        public bool IsTaxable { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public string[] Images { get; set; }

        public bool Archived { get; set; }
        public DateTimeOffset? ArchivedAt { get; set; } = DateTime.UtcNow;
    }
}
