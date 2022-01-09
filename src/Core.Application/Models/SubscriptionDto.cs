using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public DateTime? ValidAt { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsCanceled { get; set; }
    }
}
