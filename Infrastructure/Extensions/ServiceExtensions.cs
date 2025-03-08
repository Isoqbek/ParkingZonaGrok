using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IParkingSpotRepository, ParkingSpotRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Services (Application Layer)
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IParkingSpotService, ParkingSpotService>();
        services.AddScoped<IParkingZoneService, ParkingZoneService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
