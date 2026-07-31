using PetHealth.Application.Commands.Treatments;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Treatments;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface ITreatmentRepository:
    IQueryHandlerAsync<GetAllTreatmentQueryAsync, Result<IEnumerable<Treatment>>>,
    IQueryHandlerAsync<GetAllByUserTreatmentQueryAsync, Result<IEnumerable<Treatment>>>,
    IQueryHandlerAsync<GetByPetIdTreatmentQueryAsync, Result<IEnumerable<Treatment>>>,
    ICommandHandlerAsync<UpdateTreatmentCommandAsync, Result>,
    ICommandHandlerAsync<InsertTreatmentCommandAsync, Result>,
    ICommandHandlerAsync<DeleteTreatmentCommandAsync, Result>
{
    
}