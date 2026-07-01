using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PetHealth.Application.Security;
using PetHealth.Infrastructure.Extensions;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;


var builder = WebApplication.CreateBuilder(args);

#region JWT

//Configuration de JwtSettings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

//Ajouter l'authentification Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                               throw new ArgumentNullException())),
        RoleClaimType = "Role"
    });

#endregion

#region LOGING

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() //minimum global
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Error)
    //Enrichissement
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
    .Enrich.WithProperty("AppEnv", builder.Environment.EnvironmentName)
    //Sinks - Destinations (Nuget Serilog.Sinks.File)
    .WriteTo.File(
        path: @"logs/application.log",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}[{Level:u3}] - {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 10,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 40 * 1024 * 1024,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .WriteTo.File(
        path: @"logs\Fatal-application.log",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}[{Level:u3}] - {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 10,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 40 * 1024 * 1024,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Fatal
    )
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();
//Remplacer le logger microsoft par mon serilog (Nuget : SeriLog.AspNetCore)
builder.Host.UseSerilog();

#endregion

//Récupération de la connection strings dans les user-secrets
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbConnection(connection ?? throw new ArgumentNullException());

builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
Log.Information("Application started");
app.Run();