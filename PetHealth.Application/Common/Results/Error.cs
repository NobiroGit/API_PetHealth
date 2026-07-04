namespace PetHealth.Application.Common.Results;

public record Error(string Code, string Message)
{
    // Erreurs génériques réutilisables
    public static readonly Error NotFound = new("404", "Resource not found");
    public static readonly Error Unexpected = new("500", "An unexpected error occurred");

    /// <summary>
    /// Constructeur qui accepte les SqlExceptions
    /// </summary>
    /// <param name="sqlExtension"></param>
    /// <returns>Error</returns>
    public Error(string sqlExtension): this("SqlException", sqlExtension)
    {
    }

    public Error(Exception ex): this("Exception", ex.Message)
    {
    }
}
