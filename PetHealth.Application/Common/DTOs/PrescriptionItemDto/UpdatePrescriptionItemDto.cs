using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.PrescriptionItemDto;

public class UpdatePrescriptionItemDto
{
    [Required] public int Id { get; init; }

    [Required] public int PrescriptionId { get; init; }

    [Required] public int MedicineId { get; init; }

    [Required] [MaxLength(500)] public string Dosage { get; init; }

    [Required] public int DurationDays { get; init; }

    [Required] [MaxLength(500)] public string Instructions { get; init; }
}