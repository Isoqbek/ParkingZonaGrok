using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ParkingSpotRepository : IParkingSpotRepository
{
    private readonly ApplicationDbContext _context;

    public ParkingSpotRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ParkingSpot>> GetAllAsync() =>
        await _context.ParkingSpots.ToListAsync();

    public async Task<ParkingSpot?> GetByIdAsync(int id) =>
        await _context.ParkingSpots.FindAsync(id);

    public async Task AddAsync(ParkingSpot spot)
    {
        await _context.ParkingSpots.AddAsync(spot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ParkingSpot spot)
    {
        _context.ParkingSpots.Update(spot);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var spot = await _context.ParkingSpots.FindAsync(id);
        if (spot != null)
        {
            _context.ParkingSpots.Remove(spot);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ParkingSpot> GetAvailableSpotAsync()
    {
        var spot = await _context.ParkingSpots.FirstOrDefaultAsync(s => !s.IsOccupied);

        if (spot == null)
        {
            throw new Exception("No available parking spots.");
        }

        return spot;
    }

    public async Task<int> GetSpotCountAsync()
    {
        return await _context.ParkingSpots.CountAsync();
    }

}
