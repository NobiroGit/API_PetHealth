using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.TreatmentDto;

public class UpdateTreatmentDto
{
    [Required]
    public int Id { get; init; }
    
    [Required]
    public int PetId { get; init; }
    
    [Required]
    public int PrescriptionItemId { get; init; }
    
    [Required]
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}