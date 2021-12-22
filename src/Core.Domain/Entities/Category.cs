using Core.Domain.Enitities;
using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId {  get; set; }
        public string CategoryName { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
