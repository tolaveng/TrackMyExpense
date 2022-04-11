using Core.Domain.Enums;
using FluentValidation;

namespace Core.Application.Models
{
    public class SubscriptionDto : Auditable<Guid>
    {
        public Guid UserId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Decimal PaidAmount { get; set; }
        public DateTime? PaidDate { get; set; }
        public string PaidGateway { get; set; } = string.Empty;
        public string PaidRef { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentCycle PaymentCycle { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }
        public bool IsCanceled { get; set; }
    }

    public class SubscriptionValidator : AbstractValidator<SubscriptionDto>
    {
        public SubscriptionValidator()
        {
            RuleFor(x => x.PaidAmount)
                .NotEmpty().When(x => x.SubscriptionType == SubscriptionType.Premium)
                .WithMessage("Paid Amount cannot be empty or 0.")
                .GreaterThan(0)
                .When(x => x.SubscriptionType == SubscriptionType.Premium)
                .WithMessage("Paid Amount must be greater than 0.");
            RuleFor(x => x.Begin).NotEmpty().WithMessage("Begin date cannot be empty");
            RuleFor(x => x.End.Value).GreaterThanOrEqualTo(x => x.Begin)
                .When(x => x.End.HasValue)
                .WithMessage("End date must be after the Begin date.");
        }
    }
}
