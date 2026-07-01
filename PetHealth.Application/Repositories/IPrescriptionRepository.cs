using PetHealth.Application.Commands.Prescriptions;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.Prescriptions;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface IPrescriptionRepository: 
    IQueryHandlerAsync<GetAllPrescriptionQueryAsync, Result<IEnumerable<Prescription>>>,
    IQueryHandlerAsync<GetPrescriptionByIdQueryAsync, Result<IEnumerable<Prescription>>>,
    ICommandHandlerAsync<InsertPrescriptionCommandAsync, Result<int>>,
    ICommandHandlerAsync<DeletePrescriptionCommandAsync, Result>

{
    
}