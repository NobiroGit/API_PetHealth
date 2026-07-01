using System.Windows.Input;
using PetHealth.Application.Commands.Appointment;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppointmentsDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Appointment;

namespace PetHealth.Application.Repositories;

public interface IAppointmentRepository:
    IQueryHandlerAsync<GetAllAppointmentQueryAsync, Result<IEnumerable<AppointmentDto>>>,
    IQueryHandlerAsync<GetByPetIdAppointmentQueryAsync, Result<IEnumerable<AppointmentDto>>>,
    ICommandHandlerAsync<InsertAppointmentCommandAsync, Result>,
    ICommandHandlerAsync<UpdateAppointmentCommandAsync, Result>,
    ICommandHandlerAsync<DeleteAppointmentCommandAsync, Result>
{
    
}