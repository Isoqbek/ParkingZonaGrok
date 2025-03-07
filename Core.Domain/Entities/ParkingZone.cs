namespace Core.Domain.Entities;

public class ParkingZone
{
    public int Id { get; set; }
    public int TotalSpots { get; set; }
    public int AvailableSpots { get; set; }
    public decimal HourlyRate { get; set; } // soatlik narx
}
