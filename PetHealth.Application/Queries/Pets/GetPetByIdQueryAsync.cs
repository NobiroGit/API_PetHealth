using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PetsDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Queries.Pets;

public class GetPetByIdQueryAsync: IQueryDefinitionAsync<Result<PetDto?>>
{
    public int Id { get; }

    public GetPetByIdQueryAsync(int id)
    {
        Id = id;
    }
}