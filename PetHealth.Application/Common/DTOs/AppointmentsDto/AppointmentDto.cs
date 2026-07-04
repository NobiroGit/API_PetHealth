namespace PetHealth.Application.Common.DTOs.AppointmentsDto;

public class AppointmentDto
{
    public int Id { get; init; }
    public int PetId { get; init; }
    public int VetId { get; init; }
    public DateTimeOffset AppointmentDate { get; init; }
    public string Reason { get; init; } = string.Empty;
    public string? Diagnosis { get; init; }
    public string? Notes { get; init; }
    public decimal? Cost { get; init; }
    public string Status { get; init; }= string.Empty;
}