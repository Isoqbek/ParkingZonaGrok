using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IParkingSpotRepository
{
    Task<IEnumerable<ParkingSpot>> GetAllAsync();
    Task<ParkingSpot?> GetByIdAsync(int id);
    Task AddAsync(ParkingSpot spot);
    Task UpdateAsync(ParkingSpot spot);
    Task DeleteAsync(int id);
}
