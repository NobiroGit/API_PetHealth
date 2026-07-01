using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.AppUserDto;

public class LoginAppUserDto
{
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; init; }
    
    [Required]
    public string Password { get; init; }
}