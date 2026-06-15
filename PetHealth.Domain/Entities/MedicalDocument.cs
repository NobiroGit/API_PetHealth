namespace PetHealth.Domain.Entities;

public class MedicalDocument
{
    //Properties
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string DocumentType { get; set; } = string.Empty; //'Xray', 'BloodTest', 'Ultrasound'…
    public string FileUrl { get; set; } = string.Empty;
    public DateOnly DocumentDate { get; set; }
    public string? Description { get; set; }
    
}
