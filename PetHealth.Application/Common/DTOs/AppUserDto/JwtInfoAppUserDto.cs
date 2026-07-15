namespace PetHealth.Application.Common.DTOs.AppUserDto;

public class JwtInfoAppUserDto
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string Role { get; init; }
    public required string Email { get; init; }
}