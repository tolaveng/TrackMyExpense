using Core.Domain.Enitities;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class RecurrentExpense : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Ref { get; set; }
        public string Payee { get; set; }
        public bool IsTaxable { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Guid BudgetJarId { get; set; }
        public virtual BudgetJar BudgetJar { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public RepeatType Repeat { get; set; }
        public int RepeatDay { get; set; }
        public string RepeatDaily { get; set; }     // Json {Mon,Tue,...} = {true,false,...}
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
