using PetHealth.Application.Commands.PrescriptionsItem;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.PrescriptionsItem;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface IPrescriptionItemRepository: 
    IQueryHandlerAsync<GetAllPrescriptionItemQueryAsync, Result<IEnumerable<PrescriptionItem>>>,
    IQueryHandlerAsync<GetPrescriptionItemByIdQueryAsync, Result<IEnumerable<PrescriptionItem>>>,
    ICommandHandlerAsync<InsertPrescriptionItemCommandAsync, Result>,
    ICommandHandlerAsync<UpdatePrescriptionItemCommandAsync, Result>,
    ICommandHandlerAsync<DeletePrescriptionItemCommandAsync, Result>
{
    
}