using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserRoleDto;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.AppUserRoles;

public class GetAllAppUserRoleQueriesAsync: IQueryDefinitionAsync<Result<IEnumerable<AppUserRoleDto>>>
{
    
}