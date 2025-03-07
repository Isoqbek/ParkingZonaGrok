using Core.Application.DTOs;
using Core.Application.Interfaces;
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
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleById(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
            return NotFound();

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
    {
        await _vehicleService.AddVehicleAsync(vehicleDto);
        return CreatedAtAction(nameof(GetVehicleById), new { id = vehicleDto.Id }, vehicleDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleDto vehicleDto)
    {
        if (id != vehicleDto.Id)
            return BadRequest("ID mismatch");

        await _vehicleService.UpdateVehicleAsync(vehicleDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        await _vehicleService.RemoveVehicleAsync(id);
        return NoContent();
    }
}
