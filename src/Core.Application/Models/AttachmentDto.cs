using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class AttachmentDto : EntityDto<Guid>
    {
        public string Name { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
    }
}
