using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.PrescriptionsItem;

public class GetAllPrescriptionItemQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<PrescriptionItem>>>
{
    
}