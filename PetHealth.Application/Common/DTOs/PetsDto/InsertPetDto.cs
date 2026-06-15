using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.PetsDto;

public class InsertPetDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int OwnerId { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100, ErrorMessage = "Species must not exceed 100 characters")]
    public string Species { get; set; } = string.Empty;

    [Required]
    [MaxLength(100, ErrorMessage = "Breed must not exceed 100 characters")]
    public string Breed { get; set; } = string.Empty;

    [Required] public DateOnly BirthDate { get; set; }

    [Required]
    [RegularExpression(@"^[MFU]$", ErrorMessage = "Gender must be M, F or U.")]
    public char Gender { get; set; } // 'M' / 'F' / 'U'

    [Required]
    [StringLength(15, MinimumLength = 9)]
    [RegularExpression(@"^\d{9,15}$", ErrorMessage = "Microchip number must contain between 9 and 15 digits.")]
    public string MicrochipNumber { get; set; } = string.Empty;
}