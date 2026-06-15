namespace PetHealth.Domain.Entities;

public class Pet
{
    //Properties
    public int Id { get; init; }
    public int OwnerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Pseudo { get; init; } = string.Empty;
    public string Species { get; init; } = string.Empty;
    public string Breed { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
    public char Gender { get; init; } // 'M' / 'F' / 'U'
    public string MicrochipNumber { get; init; } = string.Empty;
    public string? PhotoUrl { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    
}
