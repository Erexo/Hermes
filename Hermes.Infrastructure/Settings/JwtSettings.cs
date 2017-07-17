namespace Hermes.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string PrivateKey { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
