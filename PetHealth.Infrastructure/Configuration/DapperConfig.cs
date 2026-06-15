using Dapper;
using PetHealth.Infrastructure.TypeHandlers;

namespace PetHealth.Infrastructure.Configuration;

public static class DapperConfig
{
    private static bool _initialized = false;

    public static void RegisterTypeHandlers()
    {
        if (_initialized) return;

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        // SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler()); // si besoin plus tard

        _initialized = true;
    }
}