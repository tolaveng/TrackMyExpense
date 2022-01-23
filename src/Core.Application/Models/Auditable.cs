using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class Auditable
    {
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Archived { get; set; }
    }
}
