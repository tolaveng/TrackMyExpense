using Core.Domain.Enitities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class BudgetJar : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }
        public float Percentage { get; set; }
        public Guid IconId { get; set; }
        public virtual Icon Icon { get; set; }
        public bool IsDefault { get; set; }
        public bool Archived { get; set; }
        public Decimal TotalBalance { get; set; }
    }
}
