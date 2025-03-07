namespace Core.Application.DTOs;

public class VehicleDto
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = null!;
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public bool PaymentStatus { get; set; }
}
