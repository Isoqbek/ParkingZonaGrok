using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IParkingZoneService
{
    Task<ParkingZoneDto?> GetZoneByIdAsync(int id);
    Task UpdateZoneAsync(ParkingZoneDto zoneDto);
}
