using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Appointment;

public class DeleteAppointmentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeleteAppointmentCommandAsync(int id)
    {
        Id = id;
    }
}