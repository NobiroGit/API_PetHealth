namespace PetHealth.Domain.Entities;

public class Prescription
{
    //Properties
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public DateOnly IssueDate { get; set; }
}
