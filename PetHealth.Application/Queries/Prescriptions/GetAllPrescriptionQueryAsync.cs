using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.Prescriptions;

public class GetAllPrescriptionQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<Prescription>>>
{
    
}