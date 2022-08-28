namespace Core.Infrastructure.Configurations
{
    public class OidcSetting
    {
        public string ClientId { get; set; } = "api-client";
        public string ClientSecret { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public int AccessTokenLifetimeMinutes { get; set; }
        public int RefreshTokenLifetimeDays { get; set; }
    }
}
