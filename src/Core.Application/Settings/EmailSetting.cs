using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Settings
{
    public class EmailSetting
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
