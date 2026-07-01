using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.Medicines;

public class GetAllMedicineQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<Medicine>>>
{
    
}