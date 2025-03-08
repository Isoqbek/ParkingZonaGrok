namespace Core.Application.DTOs.VehivleDTOs;

public class VehicleDto
{

    public string LicensePlate { get; set; } = null!;
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public bool PaymentStatus { get; set; }
}
