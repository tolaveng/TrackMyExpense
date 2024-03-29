﻿namespace Core.Application.Mediator.Expenses
{
    public class ExpenseFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid BudgetJarId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; } = String.Empty;
        public bool IsTaxable { get; set; }
    }
}
