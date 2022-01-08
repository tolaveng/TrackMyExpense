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
        public DateTime? ValidAt { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsCanceled { get; set; }
    }
}
