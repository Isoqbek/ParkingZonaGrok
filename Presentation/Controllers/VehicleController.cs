using Core.Application.DTOs;
using Core.Application.DTOs.VehivleDTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> GetVehicleById(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
            return NotFound();

        return Ok(vehicle);
    }

    [HttpPost("entry")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> RegisterVehicleEntry([FromBody] VehicleExitDto vehicleDto)
    {
        try
        {
            await _vehicleService.AddVehicleAsync(vehicleDto);
            return Ok("Vehicle entered and spot assigned.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("exit/{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> RegisterVehicleExit(string lisencePlate)
    {
        try
        {
            var vehicle = await _vehicleService.GetVehicleByLicensePlateAsync(lisencePlate);
            if (vehicle == null)
                return NotFound();

            var updateDto = new VehicleUpdateDto
            {
                LicensePlate = vehicle.LicensePlate,
                EntryTime = vehicle.EntryTime,
                ExitTime = DateTime.Now,
                PaymentStatus = vehicle.PaymentStatus
            };

            await _vehicleService.UpdateVehicleAsync(updateDto);
            return Ok("Vehicle exited and spot freed.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        await _vehicleService.RemoveVehicleAsync(id);
        return NoContent();
    }
}
