using PetHealth.Application.Common.Cqs;
using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Application.Common.Results;

namespace PetHealth.Application.Commands.AppUsers;

public class UpdateAppUserCommandAsync: ICommandDefinitionAsync<Result>
{
    public int Id { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Phone { get; init; }
    public string Address { get; init; }
    public bool IsActive { get; init; }
    public string? Specialty { get; init; }

    public UpdateAppUserCommandAsync(UpdateAppUserDto dto, int id)
    {
        Id = id;
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Email = dto.Email;
        Password = dto.Password;
        Phone = dto.Phone;
        Address = dto.Address;
        IsActive = dto.IsActive;
        Specialty = dto.Specialty;
    }
}