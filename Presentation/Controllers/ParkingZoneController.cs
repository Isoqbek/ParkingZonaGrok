using Core.Application.DTOs;
using Core.Application.Interfaces;
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
    public async Task<IActionResult> GetZoneById(int id)
    {
        var zone = await _zoneService.GetZoneByIdAsync(id);
        if (zone == null)
            return NotFound();

        return Ok(zone);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateZone(int id, [FromBody] ParkingZoneDto zoneDto)
    {
        if (id != zoneDto.Id)
            return BadRequest("ID mismatch");

        await _zoneService.UpdateZoneAsync(zoneDto);
        return NoContent();
    }
}
