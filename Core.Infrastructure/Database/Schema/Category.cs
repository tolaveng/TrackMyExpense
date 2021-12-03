using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Schema
{
    public class Category : IArchivable
    {
        [Key]
        public int CategoryId {  get; set; }

        [Required]
        [MaxLength(128)]
        public string CategoryName { get; set; }

        public string IconName { get; set; }

        //public virtual ICollection<Expense> Expenses { get; set; }

        public bool Archived { get; set; }
        public DateTimeOffset? ArchivedAt { get; set; } = DateTime.UtcNow;
    }
}
