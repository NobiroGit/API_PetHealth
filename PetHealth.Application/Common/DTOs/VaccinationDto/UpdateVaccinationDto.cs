using System.ComponentModel.DataAnnotations;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Common.DTOs.VaccinationDto;

public class UpdateVaccinationDto
{
    [Required]
    public int PetId { get; init; }
    
    [Required]
    [MaxLength(200)]
    public string VaccineName { get; init; } = "";
    
    [Required]
    public DateOnly VaccinationDate { get; init; }
    
    public DateOnly? NextBoosterDate { get; init; }
    
    [Required]
    [MaxLength(100)]
    public string BatchNumber { get; init; } = "";
}