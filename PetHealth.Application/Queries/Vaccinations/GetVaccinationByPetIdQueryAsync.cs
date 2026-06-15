using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.VaccinationDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Queries.Vaccinations;

public class GetVaccinationByPetIdQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<VaccinationDto>>>
{
    public int PetId { get; init; }

    public GetVaccinationByPetIdQueryAsync(int petId)
    {
        PetId = petId;
    }
    
}