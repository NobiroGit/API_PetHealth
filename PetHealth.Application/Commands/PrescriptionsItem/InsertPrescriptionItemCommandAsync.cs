using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PrescriptionItemDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.PrescriptionsItem;

public class InsertPrescriptionItemCommandAsync: ICommandDefinitionAsync<Result>
{
    public int PrescriptionId { get; init; }
    public int MedicineId { get; init; }
    public string Dosage { get; init; }
    public int DurationDays { get; init; }
    public string Instructions { get; init; }

    public InsertPrescriptionItemCommandAsync(InsertPrescriptionItemDto dto)
    {
        PrescriptionId = dto.PrescriptionId;
        MedicineId = dto.MedicineId;
        Dosage = dto.Dosage;
        DurationDays = dto.DurationDays;
        Instructions = dto.Instructions;
    }
}