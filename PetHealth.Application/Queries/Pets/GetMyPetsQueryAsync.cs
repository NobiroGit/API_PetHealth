using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Queries.Pets;

public class GetMyPetsQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<PetDto>>>
{
    
}