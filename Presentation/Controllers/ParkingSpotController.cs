using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingSpotController : ControllerBase
{
    private readonly IParkingSpotService _spotService;
    private readonly IParkingZoneService _zoneService;

    public ParkingSpotController(IParkingSpotService spotService, IParkingZoneService zoneService)
    {
        _spotService = spotService;
        _zoneService = zoneService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpots()
    {
        var spots = await _spotService.GetAllSpotsAsync();
        return Ok(spots);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpotById(int id)
    {
        var spot = await _spotService.GetSpotByIdAsync(id);
        if (spot == null)
            return NotFound();

        return Ok(spot);
    }

    [HttpPost]
    //[Authorize(Roles = "SuperAdmin")]  // Uncomment this line to restrict access to SuperAdmin only
    public async Task<IActionResult> AddSpot([FromBody] ParkingSpotDto spotDto)
    {
        await _spotService.AddSpotAsync(spotDto);
        return CreatedAtAction(nameof(GetSpotById), new { id = spotDto.Id }, spotDto);
    }

    [HttpPost("sync-spots/{zoneId}")]
    //[Authorize(Roles = "SuperAdmin")] // Uncomment this line to restrict access to SuperAdmin only
    public async Task<IActionResult> SyncSpotsWithTotal(int zoneId)
    {
        var zone = await _zoneService.GetZoneByIdAsync(zoneId);
        if (zone == null)
            return NotFound("Parking zone not found.");

        await _zoneService.UpdateZoneAsync(zone);
        return Ok("Parking spots synchronized with TotalSpots.");
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin,SuperAdmin")] // Uncomment this line to restrict access to Admin and SuperAdmin only
    public async Task<IActionResult> UpdateSpot(int id, [FromBody] ParkingSpotDto spotDto)
    {
        if (id != spotDto.Id)
            return BadRequest("ID mismatch");

        await _spotService.UpdateSpotAsync(spotDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> RemoveSpot(int id)
    {
        await _spotService.RemoveSpotAsync(id);
        return NoContent();
    }
}
