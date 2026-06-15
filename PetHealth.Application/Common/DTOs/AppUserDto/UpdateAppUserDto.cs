using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.AppUserDto;

public class UpdateAppUserDto
{
    [Required]
    [MaxLength(100)]
    public string LastName { get; init; } = "";
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; init; } = "";
    
    [Required]
    [MaxLength(255)]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Invalid Email Format")]
    public string Email { get; init; } = "";
    
    [Required]
    [MinLength(10)]
    public string Password { get; init; } = "";
    
    [Required]
    [MaxLength(20)]
    [RegularExpression(@"^\d{8,15}$")]
    public string Phone { get; init; } = "";
    
    [Required]
    [MaxLength(500)]
    public string Address { get; init; } = "";
    
    [Required]
    public bool IsActive { get; init; }

    public string? Specialty { get; init; }
}