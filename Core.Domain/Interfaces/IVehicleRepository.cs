using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
    Task AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(int id);
}
