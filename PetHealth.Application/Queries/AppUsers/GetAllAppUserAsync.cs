using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;


namespace PetHealth.Application.Queries.AppUsers;

public class GetAllAppUserAsync: IQueryDefinitionAsync<Result<IEnumerable<AppUserDto>>>
{
}