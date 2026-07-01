using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.MedicalDocumentDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.MedicalDocuments;

public class InsertMedicalDocumentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int AppointmentId { get; init; }

    public string DocumentType { get; init; }

    public string FileUrl { get; init; }

    public DateTimeOffset DocumentDate { get; init; }

    public string? Description { get; init; }

    public InsertMedicalDocumentCommandAsync(InsertMedicalDocumentDto dto)
    {
        AppointmentId = dto.AppointmentId;
        DocumentType = dto.DocumentType;
        FileUrl = dto.FileUrl;
        DocumentDate = dto.DocumentDate;
        Description = dto.Description;
    }
}