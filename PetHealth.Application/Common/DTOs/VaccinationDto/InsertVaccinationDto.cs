using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.VaccinationDto;

public class InsertVaccinationDto
{
    [Required] public int PetId { get; init; }

    [Required] public int VetId { get; init; }

    [Required] [MaxLength(200)] public string VaccineName { get; init; } = "";

    [Required] public DateOnly VaccinationDate { get; init; }

    public DateOnly? NextBoosterDate { get; init; }

    [Required] [MaxLength(100)] public string BatchNumber { get; init; } = "";
}