namespace School.Data.Helper;
public class JwtSettings
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public bool validateAudience { get; set; }
    public bool validateIssuer { get; set; }
    public bool validateLifetime { get; set; }
    public bool validateIssuerSigningKey { get; set; }
}
