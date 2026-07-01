using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PrescriptionItemDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.PrescriptionsItem;

public class UpdatePrescriptionItemCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; set; }
    public int PrescriptionId { get; set; }
    public int MedicineId { get; set; }
    public string Dosage { get; set; }
    public int DurationDays { get; set; }
    public string Instructions { get; set; }

    public UpdatePrescriptionItemCommandAsync(UpdatePrescriptionItemDto dto)
    {
        Id = dto.Id;
        PrescriptionId = dto.PrescriptionId;
        MedicineId = dto.MedicineId;
        Dosage = dto.Dosage;
        DurationDays = dto.DurationDays;
        Instructions = dto.Instructions;
    }
}