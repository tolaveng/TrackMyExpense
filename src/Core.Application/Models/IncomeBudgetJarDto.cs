using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class IncomeBudgetJarDto
    {
        public Guid IncomeId { get; set; }
        public Guid BudgetJarId { get; set; }
        public decimal Amount { get; set; }
        public float Percentage { get; set; }

        public IncomeDto Income { get; set; }
        public BudgetJarDto BudgetJar { get; set; }
    }
}
