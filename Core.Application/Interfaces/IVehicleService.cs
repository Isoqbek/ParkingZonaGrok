using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
    Task<VehicleDto?> GetVehicleByIdAsync(int id);
    Task AddVehicleAsync(VehicleDto vehicleDto);
    Task UpdateVehicleAsync(VehicleDto vehicleDto);
    Task RemoveVehicleAsync(int id);
}
