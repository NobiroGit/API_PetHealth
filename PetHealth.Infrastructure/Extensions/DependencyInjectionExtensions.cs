using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using PetHealth.Application.Repositories;
using PetHealth.Infrastructure.Configuration;
using PetHealth.Infrastructure.Services;

namespace PetHealth.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDbConnection(this IServiceCollection services, string connectionString)
    {
        DapperConfig.RegisterTypeHandlers();
        services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
        services.AddScoped<IPetRepository, PetService>();
        services.AddScoped<IAppUserRepository, AppUserService>();
        services.AddScoped<ICurrentUserRepository, CurrentUserRepository>();
        services.AddScoped<IAppUserRoleRepository, AppUserRoleService>();
        services.AddScoped<IWeightRecordRepository, WeightRecordService>();
        services.AddScoped<IVaccinationRepository, VaccinationService>();
        services.AddScoped<IMedicalDocumentRepository, MedicalDocumentService>();
        services.AddScoped<ITreatmentRepository, TreatmentService>();
        services.AddScoped<IAppointmentRepository, AppointmentService>();
        services.AddScoped<PrescriptionService>();
        services.AddScoped<IPrescriptionRepository>(sp => sp.GetRequiredService<PrescriptionService>());
        services.AddScoped<IPrescriptionItemRepository>(sp => sp.GetRequiredService<PrescriptionService>());
        services.AddScoped<IMedicineRepository, MediceService>();
        return services;
    }
}