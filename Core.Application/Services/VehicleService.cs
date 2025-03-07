using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles.Select(v => new VehicleDto
        {
            Id = v.Id,
            LicensePlate = v.LicensePlate,
            EntryTime = v.EntryTime,
            ExitTime = v.ExitTime,
            PaymentStatus = v.PaymentStatus
        });
    }

    public async Task<VehicleDto?> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        return vehicle == null ? null : new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            EntryTime = vehicle.EntryTime,
            ExitTime = vehicle.ExitTime,
            PaymentStatus = vehicle.PaymentStatus
        };
    }

    public async Task AddVehicleAsync(VehicleDto vehicleDto)
    {
        var vehicle = new Vehicle
        {
            LicensePlate = vehicleDto.LicensePlate,
            EntryTime = vehicleDto.EntryTime,
            PaymentStatus = vehicleDto.PaymentStatus
        };
        await _vehicleRepository.AddAsync(vehicle);
    }

    public async Task UpdateVehicleAsync(VehicleDto vehicleDto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleDto.Id);
        if (vehicle == null) return;

        vehicle.LicensePlate = vehicleDto.LicensePlate;
        vehicle.ExitTime = vehicleDto.ExitTime;
        vehicle.PaymentStatus = vehicleDto.PaymentStatus;

        await _vehicleRepository.UpdateAsync(vehicle);
    }

    public async Task RemoveVehicleAsync(int id)
    {
        await _vehicleRepository.DeleteAsync(id);
    }
}
