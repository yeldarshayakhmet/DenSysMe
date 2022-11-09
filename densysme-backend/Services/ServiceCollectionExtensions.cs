using Core.DataTransfer.User;
using Microsoft.Extensions.DependencyInjection;
using Services.Doctor;
using Services.User;

namespace Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IRequestValidator<LoginDto>, LoginValidator>();
        return services;
    }
}