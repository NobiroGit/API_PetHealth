using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Queries.Appointment;

public class GetByPetIdAppointmentQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<AppointmentDto>>>
{
    public int PetId { get; init; }

    public GetByPetIdAppointmentQueryAsync( int petId)
    {
        PetId = petId;
    }
}