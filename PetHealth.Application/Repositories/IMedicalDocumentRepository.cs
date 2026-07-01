using PetHealth.Application.Commands.MedicalDocuments;
using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;
using PetHealth.Application.Queries.MedicalDocuments;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Repositories;

public interface IMedicalDocumentRepository :
    IQueryHandlerAsync<GetAllMedicalDocumentQueryAsync, Result<IEnumerable<MedicalDocument>>>,
    IQueryHandlerAsync<GetAllByUserMedicalDocumentQueryAsync, Result<IEnumerable<MedicalDocument>>>,
    ICommandHandlerAsync<UpdateMedicalDocumentCommandAsync, Result>,
    ICommandHandlerAsync<InsertMedicalDocumentCommandAsync, Result>,
    ICommandHandlerAsync<DeleteMedicalDocumentCommandAsync, Result>
{
    
}