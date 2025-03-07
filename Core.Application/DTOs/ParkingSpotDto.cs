namespace Core.Application.DTOs;

public class ParkingSpotDto
{
    public int Id { get; set; }
    public string SpotNumber { get; set; } = null!;
    public bool IsOccupied { get; set; }
}
