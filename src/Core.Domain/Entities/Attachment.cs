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
    public class Attachment : BaseEntity<Guid>
    {
        public Guid ExpenseId { get; set; }

        /* Friendly Name */
        [MaxLength(128)]
        public string Name { get; set; }

        /* FileName only, not include directory or path */
        public string FileName { get; set; }

        public long FileSize { get; set; }
    }
}
