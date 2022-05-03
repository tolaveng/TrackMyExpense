using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class ConsolidateBudgetJar
    {
        [MaxLength(128)]
        [Key]
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Decimal Amount { get; set; }
        public Guid IconId { get; set; }
        public virtual Icon Icon { get; set; }
    }
}
