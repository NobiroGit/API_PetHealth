using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.Treatments;

public class GetAllByUserTreatmentQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<Treatment>>>
{
    
}