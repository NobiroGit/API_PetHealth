namespace PetHealth.Domain.Entities;

public class Treatment
{
    //Properties
    public int Id { get; set; }
    public int PetId { get; set; }
    public int AppointmentId { get; set; }
    public int PrescriptionId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsOngoing { get; set; } = true;

}
