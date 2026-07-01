using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Queries.MedicalDocuments;

public class GetAllByUserMedicalDocumentQueryAsync: IQueryDefinitionAsync<Result<IEnumerable<MedicalDocument>>>
{
    
}