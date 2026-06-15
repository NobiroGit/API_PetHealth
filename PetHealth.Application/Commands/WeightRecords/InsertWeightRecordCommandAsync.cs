using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.WeightRecords;

public class InsertWeightRecordCommandAsync: ICommandDefinitionAsync<Result>
{
    public int PetId { get; init; }
    public DateOnly MeasurementDate { get; init; }
    public decimal WeightKg { get; init; }

    public InsertWeightRecordCommandAsync(InsertWeightRecordDto dto)
    {
        PetId = dto.PetId;
        MeasurementDate = dto.MeasurementDate;
        WeightKg = dto.WeightKg;
    }
}