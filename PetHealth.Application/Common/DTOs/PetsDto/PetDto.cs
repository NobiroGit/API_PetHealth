namespace PetHealth.Application.Common.DTOs.PetsDto;

public class PetDto
{
    //Properties
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Pseudo { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public char Gender { get; set; } // 'M' / 'F' / 'U'
    public string MicrochipNumber { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}