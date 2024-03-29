﻿using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


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

        public string ProfileImage { get; set; }
        public string TimeZone { get; set; }
        public string CultureInfo { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public DateTime? LastSeen { get; set; }

    }
}
