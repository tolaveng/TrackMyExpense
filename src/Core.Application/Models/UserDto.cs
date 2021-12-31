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
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Subcription Subcription { get; set; }
        public decimal Wallet { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDisabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        
        public ICollection<Expense> Expenses { get; set; }
    }
}