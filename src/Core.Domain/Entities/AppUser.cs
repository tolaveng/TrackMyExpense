using Core.Domain.Enitities;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        [MaxLength(64)]
        public string Username { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [MaxLength(64)]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Password { get; set; }

        [MaxLength(128)]
        public string FullName { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public bool IsDisabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}
