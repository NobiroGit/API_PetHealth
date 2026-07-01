using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.TreatmentDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Treatments;

public class InsertTreatmentCommandAsync: ICommandDefinitionAsync<Result>
{
    public int PetId { get; init; }
    public int PrescriptionItemId { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }

    public InsertTreatmentCommandAsync(InsertTreatmentDto dto)
    {
        PetId = dto.PetId;
        PrescriptionItemId = dto.PrescriptionItemId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }
}