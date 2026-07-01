using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.PrescriptionDto;

public class InsertPrescriptionDto
{
    [Required]
    public int AppointmentId { get; init; }
    public DateOnly? IssueDate { get; init; } = null;
}