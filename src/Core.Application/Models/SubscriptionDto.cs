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
        public string PaidGateway { get; set; }
        public string PaidRef { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Currency { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
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
            RuleFor(x => x.ValidFrom).NotEmpty().WithMessage("Valid From cannot be empty");
            RuleFor(x => x.ValidTo.Value).GreaterThanOrEqualTo(x => x.ValidFrom)
                .When(x => x.ValidTo.HasValue)
                .WithMessage("Valid To must be after the valid from.");
        }
    }
}
