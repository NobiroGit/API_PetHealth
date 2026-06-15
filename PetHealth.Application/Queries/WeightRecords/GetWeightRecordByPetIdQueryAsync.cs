using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.WeightRecords;

public class GetWeightRecordByPetIdQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<WeightRecordDto>>>
{
    public int PetId { get; set; }

    public GetWeightRecordByPetIdQueryAsync(int petId)
    {
        PetId = petId;
    }
}