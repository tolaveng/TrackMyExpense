using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Application.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public bool IsDisabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Currency { get; set; }
        public string ProfileImage { get; set; }

        // resolve in mapper
        public string ProfileImageUrl { get; set; }
        public string ProfileImageThumbnailUrl { get; set; }

    }
}