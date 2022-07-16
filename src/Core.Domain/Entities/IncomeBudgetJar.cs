using System;


namespace Core.Domain.Entities
{
    public class IncomeBudgetJar
    {
        public Guid IncomeId { get; set; }
        public Guid BudgetJarId { get; set; }
        public decimal Amount { get; set; }
        public float Percentage { get; set; }

        public virtual Income Income { get; set; }
        public virtual BudgetJar BudgetJar { get; set; }
    }
}
