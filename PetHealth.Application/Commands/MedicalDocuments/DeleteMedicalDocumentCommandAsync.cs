using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.MedicalDocuments;

public class DeleteMedicalDocumentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }

    public DeleteMedicalDocumentCommandAsync(int id)
    {
        Id = id;
    }
}