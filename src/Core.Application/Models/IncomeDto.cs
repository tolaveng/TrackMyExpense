namespace Core.Application.Models
{
    public class IncomeDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; } = String.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Archived { get; set; }
    }
}
