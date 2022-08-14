﻿namespace Core.Infrastructure.Configurations
{
    public class JwtSetting
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TokenExpirationDay { get; set; }
    }
}
