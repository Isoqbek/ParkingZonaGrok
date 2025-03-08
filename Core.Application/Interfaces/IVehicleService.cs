using Core.Application.DTOs.VehivleDTOs;

namespace Core.Application.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
    Task<VehicleDto?> GetVehicleByIdAsync(int id);
    Task AddVehicleAsync(VehicleExitDto vehicleDto);
    Task UpdateVehicleAsync(VehicleUpdateDto vehicleDto);
    Task RemoveVehicleAsync(int id);
    Task<VehicleDto?> GetVehicleByLicensePlateAsync(string licensePlate); // Added this method
}
