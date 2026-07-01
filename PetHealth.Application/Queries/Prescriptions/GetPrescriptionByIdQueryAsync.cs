using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.Prescriptions;

public class GetPrescriptionByIdQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<Prescription>>>
{
    public int Id { get; init; }

    public GetPrescriptionByIdQueryAsync(int id)
    {
        Id = id;
    }
}