using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingSpotController : ControllerBase
{
    private readonly IParkingSpotService _spotService;

    public ParkingSpotController(IParkingSpotService spotService)
    {
        _spotService = spotService;
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
    public async Task<IActionResult> AddSpot([FromBody] ParkingSpotDto spotDto)
    {
        await _spotService.AddSpotAsync(spotDto);
        return CreatedAtAction(nameof(GetSpotById), new { id = spotDto.Id }, spotDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpot(int id, [FromBody] ParkingSpotDto spotDto)
    {
        if (id != spotDto.Id)
            return BadRequest("ID mismatch");

        await _spotService.UpdateSpotAsync(spotDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveSpot(int id)
    {
        await _spotService.RemoveSpotAsync(id);
        return NoContent();
    }
}
