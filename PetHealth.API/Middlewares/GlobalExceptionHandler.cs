using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace API_PetHealth.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        var (statusCode, title) = exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation error"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Not found"),
            SqlException => (StatusCodes.Status500InternalServerError, "Database error !"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        httpContext.Response.StatusCode = statusCode;

        _logger.LogCritical(exception, "Unhandled exception occurred");
        
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        { 
            Type = exception.GetType().FullName,
            Status = statusCode,
            Title = title
        }, cancellationToken);

        return true; // true = "j'ai géré l'exception, n'en fais rien d'autre"
    }
}