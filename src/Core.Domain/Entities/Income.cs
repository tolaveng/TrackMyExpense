using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Income : BaseEntity<Guid>
    {
        public Guid UserId  { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual ICollection<IncomeBudgetJar> IncomeBudgetJars { get; set; }

        public bool Archived { get; set; }
    }
}
