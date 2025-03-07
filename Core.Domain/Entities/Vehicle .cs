namespace Core.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = null!;
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public int? AssignedSpotId { get; set; }
    public bool PaymentStatus { get; set; } = false; // False = to'lov qilinmagan
}
