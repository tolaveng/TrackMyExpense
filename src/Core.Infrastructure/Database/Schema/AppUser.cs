using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Schema
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public Subcription Subcription { get; set; }
        public decimal Wallet { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDisabled { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
