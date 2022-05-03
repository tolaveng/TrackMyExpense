using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models.Interfaces
{
    public interface IBudgetJar
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public float Percentage { get; set; }
        public Decimal Amount { get; set; }
        public Guid IconId { get; set; }
        public IconDto Icon { get; set; }
    }
}
