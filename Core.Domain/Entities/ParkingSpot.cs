namespace Core.Domain.Entities;

public class ParkingSpot
{
    public int Id { get; set; }
    public string SpotNumber { get; set; } = null!;
    public bool IsOccupied { get; set; } = false; // Bandlik holati
    public int? OccupiedByVehicleId { get; set; } // Bu joyni qaysi mashina egallagan
}
