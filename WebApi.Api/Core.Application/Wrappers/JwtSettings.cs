namespace Core.Application.Wrappers;

public sealed class JwtSettings
{
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public double DurationInMinutes { get; init; }
}