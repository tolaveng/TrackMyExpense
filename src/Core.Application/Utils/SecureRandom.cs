using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Utils
{
    public static class SecureRandom
    {
        public static string Get6DigitsCode()
        {
            var random = RandomNumberGenerator.GetInt32(0, 1000000);
            return random.ToString("000000");
        }
    }
}
