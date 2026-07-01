using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.Treatments;

public class GetAllTreatmentQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<Treatment>>>
{
    
}