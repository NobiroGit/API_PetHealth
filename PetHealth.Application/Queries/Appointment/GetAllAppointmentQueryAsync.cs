using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Queries.Appointment;

public class GetAllAppointmentQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<AppointmentDto>>>
{
    
}