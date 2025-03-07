using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services;

public class ParkingSpotService : IParkingSpotService
{
    private readonly IParkingSpotRepository _spotRepository;

    public ParkingSpotService(IParkingSpotRepository spotRepository)
    {
        _spotRepository = spotRepository;
    }

    public async Task<IEnumerable<ParkingSpotDto>> GetAllSpotsAsync()
    {
        var spots = await _spotRepository.GetAllAsync();
        return spots.Select(s => new ParkingSpotDto
        {
            Id = s.Id,
            SpotNumber = s.SpotNumber,
            IsOccupied = s.IsOccupied
        });
    }

    public async Task<ParkingSpotDto?> GetSpotByIdAsync(int id)
    {
        var spot = await _spotRepository.GetByIdAsync(id);
        return spot == null ? null : new ParkingSpotDto
        {
            Id = spot.Id,
            SpotNumber = spot.SpotNumber,
            IsOccupied = spot.IsOccupied
        };
    }

    public async Task AddSpotAsync(ParkingSpotDto spotDto)
    {
        var spot = new ParkingSpot
        {
            SpotNumber = spotDto.SpotNumber,
            IsOccupied = spotDto.IsOccupied
        };
        await _spotRepository.AddAsync(spot);
    }

    public async Task UpdateSpotAsync(ParkingSpotDto spotDto)
    {
        var spot = await _spotRepository.GetByIdAsync(spotDto.Id);
        if (spot == null) return;

        spot.SpotNumber = spotDto.SpotNumber;
        spot.IsOccupied = spotDto.IsOccupied;

        await _spotRepository.UpdateAsync(spot);
    }

    public async Task RemoveSpotAsync(int id)
    {
        await _spotRepository.DeleteAsync(id);
    }
}
