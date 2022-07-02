namespace Core.Application.Mediator.Incomes
{
    public class IncomeFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public Guid BudgetJarId { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
