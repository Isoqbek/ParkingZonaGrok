using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingZoneController : ControllerBase
{
    private readonly IParkingZoneService _zoneService;

    public ParkingZoneController(IParkingZoneService zoneService)
    {
        _zoneService = zoneService;
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> GetZoneById(int id)
    {
        var zone = await _zoneService.GetZoneByIdAsync(id);
        if (zone == null)
            return NotFound();

        return Ok(zone);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateZone(int id, [FromBody] ParkingZoneDto zoneDto)
    {
        zoneDto.Id = id;

        await _zoneService.UpdateZoneAsync(zoneDto);
        return NoContent();
    }

    // ✅ Admin mashina kirganda yoki chiqqanda AvailableSpots yangilanadi
    [HttpPut("{id}/update-availability")]
    //[Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> UpdateAvailableSpots(int id, [FromBody] int change)
    {
        var result = await _zoneService.UpdateAvailableSpotsAsync(id, change);
        if (!result)
            return BadRequest("Failed to update available spots.");

        return NoContent();
    }
}
