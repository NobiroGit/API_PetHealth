namespace PetHealth.Domain.Entities;

public class Treatment
{
    //Properties
    public int Id { get; set; }
    public int PetId { get; set; }
    public int PrescriptionItemId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsOngoing { get; set; } = true;

    // Renseignés par jointure dans les procédures de lecture : sans eux, un propriétaire
    // ne verrait qu'un PrescriptionItemId brut (Medicine/PrescriptionItem sont Admin/Vet).
    public string? MedicineName { get; set; }
    public string? MedicineStrength { get; set; }
    public string? Dosage { get; set; }
    public int? DurationDays { get; set; }
    public string? Instructions { get; set; }
}
