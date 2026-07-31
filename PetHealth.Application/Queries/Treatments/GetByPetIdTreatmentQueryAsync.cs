using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.Treatments;

public class GetByPetIdTreatmentQueryAsync : IQueryDefinitionAsync<Result<IEnumerable<Treatment>>>
{
    public int PetId { get; init; }

    public GetByPetIdTreatmentQueryAsync(int petId)
    {
        PetId = petId;
    }
}
