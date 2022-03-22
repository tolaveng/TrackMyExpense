using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class CurrencyDto
    {
        public string Code { get; set; } = String.Empty;
        public string UnicodeDecimal { get; set; } = String.Empty;
        public string UnicodeHex { get; set; } = String.Empty;
        public string Text { get; set; } = String.Empty;

    }
}
