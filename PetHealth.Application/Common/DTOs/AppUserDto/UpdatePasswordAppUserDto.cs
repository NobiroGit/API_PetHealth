using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.AppUserDto;

public class UpdatePasswordAppUserDto
{
    [Required] [MinLength(10)] public string Password { get; set; } = "";
}