using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class IconDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IconType IconType { get; set; }
        public IconCategory IconCategory { get; set; }
        public int Ordinal { get; set; }
    }
}
