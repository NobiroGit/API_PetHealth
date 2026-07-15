using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using PetHealth.Application.Repositories;

namespace PetHealth.Infrastructure.Services;

public class CurrentUserRepository : ICurrentUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?
                .FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (claim is null || !int.TryParse(claim, out int userId))
                throw new UnauthorizedAccessException("User id claim missing or invalid in token.");

            return userId;
        }
    }
}