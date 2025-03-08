using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IParkingZoneService
{
    Task<ParkingZoneDto?> GetZoneByIdAsync(int id);
    Task<bool> UpdateAvailableSpotsAsync(int id, int change);
    Task UpdateZoneAsync(ParkingZoneDto zoneDto);
    Task UpdateTotalZoneAsync(ParkingZoneDto zoneDto);
}
