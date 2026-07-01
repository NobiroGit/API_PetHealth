using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.AppointmentsDto;

public class InsertAppointmentDto
{
    [Required] public int PetId { get; init; }

    [Required] public int VetId { get; init; }

    [Required] public DateTimeOffset AppointmentDate { get; init; }

    [Required] [MaxLength(500)] public string Reason { get; init; } = string.Empty;


    public string? Diagnosis { get; init; }
    public string? Notes { get; init; }
    public decimal? Cost { get; init; }
    [Required] public string Status { get; init; } = string.Empty;
}