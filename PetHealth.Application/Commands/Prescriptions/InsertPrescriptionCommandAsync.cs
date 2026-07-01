using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.PrescriptionDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.Prescriptions;

public class InsertPrescriptionCommandAsync: ICommandDefinitionAsync<Result<int>>
{
    public int AppointmentId { get; init; }
    public DateOnly? IssueDate { get; init; }

    public InsertPrescriptionCommandAsync(InsertPrescriptionDto dto)
    {
        AppointmentId = dto.AppointmentId;
        IssueDate = dto.IssueDate;
    }
}