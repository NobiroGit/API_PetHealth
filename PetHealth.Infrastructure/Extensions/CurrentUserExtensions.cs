using Dapper;
using PetHealth.Application.Repositories;

namespace PetHealth.Infrastructure.Extensions;

public static class CurrentUserExtensions
{
    public static DynamicParameters WithUser(this ICurrentUserRepository currentUserRepository, object definition)
    {
        var parameters = new DynamicParameters(definition);
        parameters.Add("UserId", currentUserRepository.UserId);
        return parameters;
    }
}