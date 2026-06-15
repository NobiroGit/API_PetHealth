using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Repositories;

namespace PetHealth.Application.Queries.Pets;

public class GetAllPetsQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<PetDto>>>
{
}