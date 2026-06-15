using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.PetsDto;

public class UpdatePetPseudoDto
{
    [MaxLength(100, ErrorMessage = "Pseudo must not exceed 100 characters")]
    public string? Pseudo { get; set; }
}