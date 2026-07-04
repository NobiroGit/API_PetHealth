namespace PetHealth.Application.Security;

public class JwtSettings
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    public int ExpirationInMinutes { get; init; }
    
}