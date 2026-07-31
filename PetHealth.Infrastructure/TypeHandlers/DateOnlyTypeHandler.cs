using System.Data;
using Dapper;

namespace PetHealth.Infrastructure.TypeHandlers;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }

    public override DateOnly Parse(object value)
    {
        // Gère le cas où la base de données renvoie un DateTimeOffset
        if (value is DateTimeOffset dateTimeOffset)
        {
            return DateOnly.FromDateTime(dateTimeOffset.DateTime);
        }

        // Gère le cas où la base de données renvoie un DateTime
        if (value is DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        // Fallback pour les autres types (ex: string) si convertible
        return DateOnly.FromDateTime(Convert.ToDateTime(value));
    }
}