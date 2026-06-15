namespace PetHealth.Domain.Entities;

public class AppUser
{
    //Properties
    public int Id { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public bool IsActive { get; init; } = true;
    public DateTime CreatedAt { get; init; }
    public string? Specialty { get; init; }

}
