using PetHealth.Application.Common.DTOs.AppUserDto;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Common.Mapping;

public static class AppUserDtoMapper
{
    public static AppUserDto toAppUserDto(this AppUser app)
    {
        return new AppUserDto()
        {
            Id = app.Id,
            LastName = app.LastName,
            FirstName = app.FirstName,
            Email = app.Email,
            Phone = app.Phone,
            Address = app.Address,
            IsActive = app.IsActive,
        };
    }
    
}