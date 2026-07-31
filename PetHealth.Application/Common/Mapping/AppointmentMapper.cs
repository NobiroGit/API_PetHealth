using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Common.Mapping;

public static class AppointmentMapper
{
    public static AppointmentDto MapToDto(this Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            PetId = appointment.PetId,
            VetId = appointment.VetId,
            AppointmentDate = appointment.AppointmentDate,
            Reason = appointment.Reason,
            Diagnosis = appointment.Diagnosis,
            Notes = appointment.Notes,
            Cost = appointment.Cost,
            Status = appointment.Status,
            VetFirstName = appointment.VetFirstName,
            VetLastName = appointment.VetLastName
        };
    }
}