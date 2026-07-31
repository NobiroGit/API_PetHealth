namespace PetHealth.Domain.Entities;

public class Appointment
{
    //Properties 
    public int Id { get; set; }
    public int PetId { get; set; }
    public int VetId { get; set; }
    public DateTimeOffset AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Notes { get; set; }
    public decimal? Cost { get; set; }
    public string Status { get; set; }= string.Empty;
    public string? VetFirstName { get; set; }
    public string? VetLastName { get; set; }

}
