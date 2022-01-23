using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Income : BaseEntity<Guid>
    {
        public Guid UserId  { get; set; }
        public decimal Amount { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public virtual ICollection<BudgetJar> BudgetJars { get; set; }
    }
}
