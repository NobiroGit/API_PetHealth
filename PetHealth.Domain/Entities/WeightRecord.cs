namespace PetHealth.Domain.Entities;

public class WeightRecord
{
    //Properties
    public int Id { get; set; }
    public int PetId { get; set; }
    public DateOnly MeasurementDate { get; set; }
    public decimal WeightKg { get; set; }

}
