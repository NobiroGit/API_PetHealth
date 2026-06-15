using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.PetsDto;

public class UpdatePetAdminDto
{
    [Range(1, int.MaxValue)]
    public int? OwnerId { get; set; }
    
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public string? Name { get; set; }
    
    [MaxLength(100, ErrorMessage = "Species must not exceed 100 characters")]
    public string? Species { get; set; }
    
    [MaxLength(100, ErrorMessage = "Breed must not exceed 100 characters")]
    public string? Breed { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }
    
    public char? Gender { get; set; } // 'M' / 'F' / 'U'
    
    [StringLength(15, MinimumLength = 9, ErrorMessage = "Microchip number must contain between 9 and 15 characters.")]
    public string? MicrochipNumber { get; set; }
    
}