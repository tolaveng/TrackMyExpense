using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{   
    public abstract class EntityDto<T>
    {
        public T Id { get; set; } = default(T);
        public bool Archived { get; set; }
    }
}
