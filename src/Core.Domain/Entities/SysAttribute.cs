using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class SysAttribute : BaseEntity<Guid>
    {
        [MaxLength(64)]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
