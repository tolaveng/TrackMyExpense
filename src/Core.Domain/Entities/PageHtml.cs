using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class PageHtml : BaseEntity<Guid>
    {
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
