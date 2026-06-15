using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.WeightRecords;

public class UpdateWeightRecordCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    public DateOnly MeasurementDate { get; init; }
    public decimal WeightKg { get; init; }

    public UpdateWeightRecordCommandAsync(UpdateWeightRecordDto dto,int id)
    {
        Id = id;
        MeasurementDate = dto.MeasurementDate;
        WeightKg = dto.WeightKg;
    }
}