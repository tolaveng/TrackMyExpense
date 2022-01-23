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
    public class BudgetJar : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? IncomeId { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public Decimal Amount { get; set; }
        public string IconName { get; set; }
        public bool IsSystem { get; set; }
    }
}
