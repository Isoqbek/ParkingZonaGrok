using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ParkingZoneRepository : IParkingZoneRepository
{
    private readonly ApplicationDbContext _context;

    public ParkingZoneRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ParkingZone?> GetByIdAsync(int id) =>
        await _context.ParkingZones.FindAsync(id);

    public async Task UpdateAsync(ParkingZone zone)
    {
        _context.ParkingZones.Update(zone);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAvailableSpotsAsync(int zoneId, int availableSpots)
    {
        var zone = await _context.ParkingZones.FindAsync(zoneId);
        if (zone != null)
        {
            zone.AvailableSpots = availableSpots;
            await _context.SaveChangesAsync();
        }
    }

}
