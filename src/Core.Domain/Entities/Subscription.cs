using Core.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Subscription : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionId { get; set; }
        public Guid UserId  { get; set; }
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
}
