using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IParkingSpotService
{
    Task<IEnumerable<ParkingSpotDto>> GetAllSpotsAsync();
    Task<ParkingSpotDto?> GetSpotByIdAsync(int id);
    Task AddSpotAsync(ParkingSpotDto spotDto);
    Task UpdateSpotAsync(ParkingSpotDto spotDto);
    Task RemoveSpotAsync(int id);
}
