using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Queries.AppUsers;

public class GetAppUserByIdAsync: IQueryDefinitionAsync<Result<AppUserDto?>>
{
    public int Id { get; set; }

    public GetAppUserByIdAsync(int id)
    {
        Id = id;
    }
}