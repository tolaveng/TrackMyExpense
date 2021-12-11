using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Enitities
{
    public abstract class BaseEntity
    {
        public bool Archived { get; set; }
    }
}
