namespace PetHealth.Application.Common.DTOs.WeightRecordDto;

public class WeightRecordDto
{
    public int Id { get; init; }
    public int PetId { get; init; }
    public DateOnly MeasurementDate { get; init; }
    public decimal WeightKg { get; init; }
}