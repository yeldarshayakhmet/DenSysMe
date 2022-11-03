using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EntityFrameworkUnitOfWork>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));
        services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        return services;
    }
}