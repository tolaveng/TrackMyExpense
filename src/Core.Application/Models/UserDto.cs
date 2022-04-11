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
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool PhoneNumberConfirmed { get; set; }
        public IEnumerable<SubscriptionDto> Subscriptions { get; set; } = Enumerable.Empty<SubscriptionDto>();
        public bool IsDisabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;

        // resolve in mapper
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string ProfileImageThumbnailUrl { get; set; } = string.Empty;

    }
}