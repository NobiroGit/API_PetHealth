namespace PetHealth.Domain.Entities;

public class Medicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ActiveIngredient { get; set; }
    public string Form { get; set; }
    public string Strength { get; set; }
}