using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedBy { get; set;}
        public DateTime ModifiedAt { get; set; }
        public DateTime ModifiedBy { get; set; }
        public bool Archived { get; set; }
    }
}
