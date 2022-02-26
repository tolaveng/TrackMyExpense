using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class CurrencyDto
    {
        public string Code { get; set; }
        public string UnicodeDecimal { get; set; }
        public string UnicodeHex { get; set; }
        public string Text { get; set; }

    }
}
