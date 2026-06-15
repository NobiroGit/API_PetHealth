using Microsoft.Data.SqlClient;

namespace PetHealth.Infrastructure.Extensions;

public static class SqlExceptionExtensions
{
    public static string ToError(this SqlException e)
    {
        return string.Join(" : ", e.Number, e.Message);
    }
}