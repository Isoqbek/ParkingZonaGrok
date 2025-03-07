using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services;

public class ParkingZoneService : IParkingZoneService
{
    private readonly IParkingZoneRepository _zoneRepository;

    public ParkingZoneService(IParkingZoneRepository zoneRepository)
    {
        _zoneRepository = zoneRepository;
    }

    public async Task<ParkingZoneDto?> GetZoneByIdAsync(int id)
    {
        var zone = await _zoneRepository.GetByIdAsync(id);
        return zone == null ? null : new ParkingZoneDto
        {
            Id = zone.Id,
            TotalSpots = zone.TotalSpots,
            AvailableSpots = zone.AvailableSpots,
            HourlyRate = zone.HourlyRate
        };
    }

    public async Task UpdateZoneAsync(ParkingZoneDto zoneDto)
    {
        var zone = await _zoneRepository.GetByIdAsync(zoneDto.Id);
        if (zone == null) return;

        zone.TotalSpots = zoneDto.TotalSpots;
        zone.AvailableSpots = zoneDto.AvailableSpots;
        zone.HourlyRate = zoneDto.HourlyRate;

        await _zoneRepository.UpdateAsync(zone);
    }
}
