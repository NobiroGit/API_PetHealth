namespace PetHealth.Domain.Entities;

public class Vaccination
{
    //Properties
    public int Id { get; set; }
    public int PetId { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateOnly VaccinationDate { get; set; }
    public DateOnly? NextBoosterDate { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    
}
