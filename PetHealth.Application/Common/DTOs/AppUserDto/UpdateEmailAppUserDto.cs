using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.AppUserDto;

public class UpdateEmailAppUserDto
{
    [Required]
    [MaxLength(255)]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Invalid Email Format")]
    public string Email { get; set; } = "";
}