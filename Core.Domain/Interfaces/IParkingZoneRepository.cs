using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IParkingZoneRepository
{
    Task<ParkingZone?> GetByIdAsync(int id);
    Task UpdateAsync(ParkingZone zone);
}
