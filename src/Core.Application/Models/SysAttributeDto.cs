using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class SysAttributeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public SysAttributeDto()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Value = string.Empty;
        }
    }
}
