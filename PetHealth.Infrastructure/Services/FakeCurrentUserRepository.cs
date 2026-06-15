using PetHealth.Application.Repositories;

namespace PetHealth.Infrastructure.Services;

public class FakeCurrentUserRepository: ICurrentUserRepository
{
    public int UserId => 1;
}