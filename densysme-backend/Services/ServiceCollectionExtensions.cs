using Core.DataTransfer.User;
using Microsoft.Extensions.DependencyInjection;
using Services.Administration;
using Services.Appointment;
using Services.Doctor;
using Services.Patient;
using Services.User;

namespace Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IAdministrationService, AdministrationService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IRequestValidator<LoginDto>, LoginValidator>();
        return services;
    }
}