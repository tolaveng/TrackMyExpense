using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class SysAttribute : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Text  { get; set; }
        public string Value { get; set; }
    }
}
