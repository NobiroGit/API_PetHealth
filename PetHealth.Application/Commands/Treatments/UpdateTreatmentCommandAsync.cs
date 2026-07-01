using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.TreatmentDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Treatments;

public class UpdateTreatmentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    public int PetId { get; init; }
    public int PrescriptionItemId { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }

    public UpdateTreatmentCommandAsync(UpdateTreatmentDto dto)
    {
        Id = dto.Id;
        PetId = dto.PetId;
        PrescriptionItemId = dto.PrescriptionItemId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }
}