using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class AttachmentDto : EntityDto<Guid>
    {
        public Guid ExpenseId { get; set; }
        public string Name { get; set; } = String.Empty;

        /* FileName only, not include directory or path */
        public string FileName { get; set; } = String.Empty;

        public long FileSize { get; set; }
    }
}
