namespace PetHealth.Domain.Entities;

public class Role
{
    //Properties
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // 'Admin', 'Vet', 'PetOwner'
    
}
