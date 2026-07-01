using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.PrescriptionsItem;

public class GetPrescriptionItemByIdQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<PrescriptionItem>>>
{
    public int Id { get; init; }

    public GetPrescriptionItemByIdQueryAsync(int id)
    {
        Id = id;
    }
}