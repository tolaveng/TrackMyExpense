using System;

namespace Core.Application.Models
{
    public class ConsolidateBudgetJarDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Decimal Amount { get; set; }
        public Guid IconId { get; set; }
        public IconDto? Icon { get; set; }
    }
}
