using MaritimeApp.Domain.Interfaces;
using MaritimeApp.Infrastructure.Data;
using MaritimeApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaritimeApp.WebAPI.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register EF Core DbContext
        services.AddDbContext<MaritimeDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Register generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Register entity-specific repositories
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPortRepository, PortRepository>();
        services.AddScoped<IShipTypeRepository, ShipTypeRepository>();
        services.AddScoped<IShipRepository, ShipRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IShipCompanyRelationRepository, ShipCompanyRelationRepository>();
        services.AddScoped<IShipSpecificationRepository, ShipSpecificationRepository>();
        services.AddScoped<IShipLocationLogRepository, ShipLocationLogRepository>();
        services.AddScoped<IVisitedCountryRepository, VisitedCountryRepository>();
        services.AddScoped<IVoyageRepository, VoyageRepository>();

        return services;
    }
}
