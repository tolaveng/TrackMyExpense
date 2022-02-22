using Core.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .NotEmpty().When(x => x.SubscriptionType == SubscriptionType.Monthly || x.SubscriptionType == SubscriptionType.Yearly)
                .WithMessage("Paid Amount cannot be empty or 0.")
                .GreaterThan(0)
                .When(x => x.SubscriptionType == SubscriptionType.Monthly || x.SubscriptionType == SubscriptionType.Yearly)
                .WithMessage("Paid Amount must be greater than 0.");

            RuleFor(x => x.ValidTo.Value).GreaterThanOrEqualTo(x => x.ValidFrom.Value)
                .When(x => x.ValidFrom.HasValue && x.ValidTo.HasValue)
                .WithMessage("Valid To must be after or equal to the valid from.");
        }
    }
}
