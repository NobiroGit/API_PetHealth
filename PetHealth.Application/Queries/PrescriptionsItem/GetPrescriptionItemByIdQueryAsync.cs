using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.PrescriptionsItem;

public class GetPrescriptionItemByIdQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<PrescriptionItem>>>
{
    public int PrescriptionId { get; init; }

    public GetPrescriptionItemByIdQueryAsync(int prescriptionId)
    {
        PrescriptionId = prescriptionId;
    }
}