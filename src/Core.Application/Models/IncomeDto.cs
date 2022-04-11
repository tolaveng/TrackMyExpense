namespace Core.Application.Models
{
    public class IncomeDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
