using PetHealth.Application.Common.DTOs.AppUserRoleDto;
using PetHealth.Domain.Entities;

namespace PetHealth.Application.Common.Mapping;

public static class AppUserRoleMapper
{
    public static AppUserRoleDto ToAppUserRoleDto(this AppUserRole app)
    {
        return new AppUserRoleDto()
        {
            RoleId = app.RoleId,
            AppUserId = app.AppUserId
        };
    }
}