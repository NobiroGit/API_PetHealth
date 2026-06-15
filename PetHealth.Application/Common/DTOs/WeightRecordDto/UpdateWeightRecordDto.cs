using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.WeightRecordDto;

public class UpdateWeightRecordDto
{
    [Required]
    public DateOnly MeasurementDate { get; init; }
    [Required]
    [Range(0.001, 999.999, ErrorMessage = "WeightKg must be between 0.001 and 999.999.")]
    public decimal WeightKg { get; init; }
}