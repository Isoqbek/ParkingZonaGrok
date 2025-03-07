namespace Core.Application.DTOs;

public class ParkingZoneDto
{
    public int Id { get; set; }
    public int TotalSpots { get; set; }
    public int AvailableSpots { get; set; }
    public decimal HourlyRate { get; set; }
}
