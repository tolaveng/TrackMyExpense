using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId {  get; set; }
        public string CategoryName { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
