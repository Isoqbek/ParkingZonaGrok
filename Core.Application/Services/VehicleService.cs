using Core.Application.DTOs.VehivleDTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IParkingSpotRepository _parkingSpotRepository;

    public VehicleService(IVehicleRepository vehicleRepository, IParkingSpotRepository parkingSpotRepository)
    {
        _vehicleRepository = vehicleRepository;
        _parkingSpotRepository = parkingSpotRepository;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles.Select(v => new VehicleDto
        {
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
            LicensePlate = vehicle.LicensePlate,
            EntryTime = vehicle.EntryTime,
            ExitTime = vehicle.ExitTime,
            PaymentStatus = vehicle.PaymentStatus
        };
    }

    public async Task<VehicleDto?> GetVehicleByLicensePlateAsync(string licensePlate)
    {
        var vehicle = await _vehicleRepository.GetByLicensePlateAsync(licensePlate);
        return vehicle == null ? null : new VehicleDto
        {
            LicensePlate = vehicle.LicensePlate,
            EntryTime = vehicle.EntryTime,
            ExitTime = vehicle.ExitTime,
            PaymentStatus = vehicle.PaymentStatus
        };
    }

    public async Task AddVehicleAsync(VehicleExitDto vehicleDto)
    {
        // Joyni band qilish
        var spot = await _parkingSpotRepository.GetAvailableSpotAsync();
        if (spot == null) throw new Exception("No available spots!");

        spot.IsOccupied = true;

        var vehicle = new Vehicle
        {
            LicensePlate = vehicleDto.LicensePlate,
            EntryTime = DateTime.UtcNow,
            PaymentStatus = false,
            AssignedSpotId = spot.Id
        };

        await _vehicleRepository.AddAsync(vehicle);
        await _parkingSpotRepository.UpdateAsync(spot);
    }

    public async Task UpdateVehicleAsync(VehicleUpdateDto vehicleDto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleDto.Id);
        if (vehicle == null) return;

        vehicle.ExitTime = DateTime.UtcNow;
        vehicle.PaymentStatus = vehicleDto.PaymentStatus;

        // Joyni bo‘shatish
        if (vehicle.AssignedSpotId.HasValue)
        {
            var spot = await _parkingSpotRepository.GetByIdAsync(vehicle.AssignedSpotId.Value);
            if (spot != null)
            {
                spot.IsOccupied = false;
                await _parkingSpotRepository.UpdateAsync(spot);
            }
        }

        vehicle.AssignedSpotId = null; // joy bo‘shatildi
        await _vehicleRepository.UpdateAsync(vehicle);
    }


    public async Task RemoveVehicleAsync(int id)
    {
        await _vehicleRepository.DeleteAsync(id);
    }
}
