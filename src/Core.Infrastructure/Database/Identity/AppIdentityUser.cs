using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Identity
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
