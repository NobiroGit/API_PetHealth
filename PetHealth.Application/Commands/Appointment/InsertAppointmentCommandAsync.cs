using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Appointment;

public class InsertAppointmentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int PetId { get; init; }
    public int VetId { get; init; }
    public DateTimeOffset AppointmentDate { get; init; }
    public string Reason { get; init; }
    public string? Diagnosis { get; init; }
    public string? Notes { get; init; }
    public decimal? Cost { get; init; }
    public string Status { get; init; }

    public InsertAppointmentCommandAsync(InsertAppointmentDto dto)
    {
        PetId = dto.PetId;
        VetId = dto.VetId;
        AppointmentDate = dto.AppointmentDate;
        Reason = dto.Reason;
        Diagnosis = dto.Diagnosis;
        Notes = dto.Notes;
        Cost = dto.Cost;
        Status = dto.Status;
    }
}