namespace ReleaseForge.Api.Auth;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = "ReleaseForge";
    public string Audience { get; set; } = "ReleaseForge";
    public int ExpirationInMinutes { get; set; } = 60;
}
