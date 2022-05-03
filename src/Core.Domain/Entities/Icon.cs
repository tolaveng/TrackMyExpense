using Core.Domain.Enitities;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Icon : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IconType IconType { get; set; }
        public bool IsHidden { get; set; }
        public bool Archived { get; set; }
    }
}
