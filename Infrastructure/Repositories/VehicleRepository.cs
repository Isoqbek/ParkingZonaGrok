using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync() =>
        await _context.Vehicles.ToListAsync();

    public async Task<Vehicle?> GetByIdAsync(int id) =>
        await _context.Vehicles.FindAsync(id);

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate) =>
        await _context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
