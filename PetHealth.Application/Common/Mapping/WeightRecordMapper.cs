using PetHealth.Application.Common.DTOs.WeightRecordDto;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Common.Mapping;

public static class WeightRecordMapper
{
    public static WeightRecordDto ToWeightRecordDto(this WeightRecord weight)
    {
        return new WeightRecordDto()
        {
            Id = weight.Id,
            PetId = weight.PetId,
            MeasurementDate = weight.MeasurementDate,
            WeightKg = weight.WeightKg
        };
    }
}